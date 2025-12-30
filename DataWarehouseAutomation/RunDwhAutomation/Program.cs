using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CommandLine;

using DataWarehouseAutomation.Utils;

using HandlebarsDotNet;

using Newtonsoft.Json.Linq;

namespace RunDwhAutomation;

class Program
{
    static int Main(string[] args)
    {
        // An optimistic start.
        Environment.ExitCode = (int)ExitCode.Success;

        // Get the handlebars extensions from the DataWarehouseAutomation class library.
        HandlebarsHelpers.RegisterHandlebarsHelpers();

        CommandLineArgumentHelper environmentHelper = new CommandLineArgumentHelper();
        //CommandLineArgumentHelper environmentHelper = new CommandLineArgumentHelper(testArgs);

        string[] localArgs = environmentHelper.args;

        Parser.Default.ParseArguments<Options>(localArgs).WithParsed(options =>
        {
            // Make sure there is a default directory set when output is enabled
            if (options.output)
            {
                options.outputDirectory ??= AppDomain.CurrentDomain.BaseDirectory;

                options.outputFileExtension ??= "txt";
            }

            // Managing verbose information back to user
            if (options.verbose)
            {
                Console.WriteLine("Verbose mode enabled.");

                if (options.input != null && options.input.Any())
                {
                    Console.WriteLine($"The input path(s) provided:");
                    foreach (var inputPath in options.input)
                    {
                        var path = Path.GetExtension(inputPath);
                        bool isInputPath = path == String.Empty;
                        Console.WriteLine($"  - {inputPath} ({(isInputPath ? "directory" : "file")})");
                    }
                }

                if (!string.IsNullOrEmpty(options.pattern))
                {
                    Console.WriteLine($"The pattern used is {options.pattern}");
                }

                if (options.recursive)
                {
                    Console.WriteLine("Recursive subdirectory search is enabled.");
                }

                if (options.combine)
                {
                    Console.WriteLine("Combine mode is enabled - all metadata files will be aggregated into a single object.");
                }

                if (options.output)
                {
                    Console.WriteLine($"Output is enabled.");
                    Console.WriteLine($"The Output Directory is {options.outputDirectory}.");
                    Console.WriteLine($"The File Extension for output file(s) is {options.outputFileExtension}");
                }

                //Console.WriteLine();
            }

            #region Core
            // Do the main stuff
            //HandlebarsHelpers.RegisterHandlebarsHelpers();

            // Collect all files from all input paths
            var allInputFiles = new List<string>();

            foreach (var inputPath in options.input)
            {
                var path = Path.GetExtension(inputPath);
                bool isPath = path == String.Empty;

                if (isPath)
                {
                    var searchOption = options.recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                    var filesInDirectory = Directory.GetFiles(inputPath, "*.json", searchOption);
                    allInputFiles.AddRange(filesInDirectory);
                }
                else
                {
                    // It's a file
                    allInputFiles.Add(inputPath);
                }
            }

            var localFiles = allInputFiles.ToArray();

            // Process the collected files
            if (options.combine)
            {
                // Combine all metadata files into a single object
                RunAutomationCombined(options, localFiles);
            }
            else
            {
                foreach (var file in localFiles)
                {
                    if (options.outputFileName == null)
                    {
                        // The output filename will be derived from the Json input (mappingName), if available.
                        RunAutomation(options, file);
                    }
                    else
                    {
                        // The output file name is concatenated as the specified file and a file number to ensure uniqueness.
                        RunAutomation(options, file, options.outputFileName + Array.IndexOf(localFiles, file));
                    }
                }
            }
            #endregion

        }).WithNotParsed(e =>
        {
            Console.WriteLine($"An error was encountered while parsing the parameters/arguments. Please review the above possible options.");
        });

        //var result = Parser.Default.ParseArguments<Options>(args);
        //var helpText = CommandLine.Text.HelpText.AutoBuild(result,
        //                                h => CommandLine.Text.HelpText.DefaultParsingErrorsHandler(result, h),
        //                                e => e);
        //Console.WriteLine(helpText);

        //Console.ReadKey();

        return Environment.ExitCode;
    }

    private static void RunAutomationCombined(Options options, string[] inputFileNames)
    {
        try
        {
            // Create the combined metadata structure
            var combinedMetadata = new JObject();
            combinedMetadata["dataObjects"] = new JArray();
            combinedMetadata["dataObjectMappingLists"] = new JArray();

            if (options.verbose)
            {
                Console.WriteLine($"Combining {inputFileNames.Length} metadata files...");
            }

            // Process each file and add to the combined structure
            foreach (var inputFileName in inputFileNames)
            {
                try
                {
                    var jsonInput = File.ReadAllText(inputFileName);
                    var parsedObject = JObject.Parse(jsonInput);

                    if (options.verbose)
                    {
                        Console.WriteLine($"Processing {Path.GetFileName(inputFileName)}...");
                    }

                    // Add dataObjects if present as an array
                    if (parsedObject["dataObjects"] != null && parsedObject["dataObjects"] is JArray)
                    {
                        foreach (var item in parsedObject["dataObjects"])
                        {
                            ((JArray)combinedMetadata["dataObjects"]).Add(item);
                        }
                    }
                    // Check if this is a standalone DataObject (has dataItems or other DataObject-specific properties)
                    else if (parsedObject["dataItems"] != null ||
                             parsedObject["dataObjectConnection"] != null ||
                             (parsedObject["name"] != null && parsedObject["dataObjectMappings"] == null))
                    {
                        // This appears to be a standalone DataObject, add it directly
                        ((JArray)combinedMetadata["dataObjects"]).Add(parsedObject);
                    }

                    // Add dataObjectMappings as a dataObjectMappingList
                    if (parsedObject["dataObjectMappings"] != null && parsedObject["dataObjectMappings"] is JArray)
                    {
                        // Wrap in a dataObjectMappingList structure, preserving all properties
                        var mappingList = new JObject();

                        // Copy all properties from the original object
                        foreach (var prop in parsedObject.Properties())
                        {
                            mappingList[prop.Name] = prop.Value;
                        }

                        // Ensure we have a name property - if not, use the filename
                        if (mappingList["name"] == null)
                        {
                            mappingList["name"] = Path.GetFileNameWithoutExtension(inputFileName);
                        }

                        ((JArray)combinedMetadata["dataObjectMappingLists"]).Add(mappingList);
                    }
                }
                catch (Exception ex)
                {
                    if (options.verbose)
                    {
                        Console.WriteLine($"Warning: Could not process file {inputFileName}: {ex.Message}");
                    }
                }
            }

            // Spool the combined metadata to disk for review
            if (options.verbose)
            {
                var debugFileName = $"{options.outputDirectory}\\combined_metadata_debug_{DateTime.Now.ToString("yyyyMMddHHmmss")}.json";
                File.WriteAllText(debugFileName, combinedMetadata.ToString(Newtonsoft.Json.Formatting.Indented));
                Console.WriteLine($"Debug: Combined metadata written to {debugFileName}");
            }

            // Process the combined metadata with the template
            var stringTemplate = File.ReadAllText(options.pattern);
            var template = Handlebars.Compile(stringTemplate);
            var result = template(combinedMetadata);

            if (options.verbose)
            {
                Console.WriteLine(result);
            }

            if (options.output)
            {
                var outputFileName = options.outputFileName ?? "combined_metadata_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                Console.WriteLine($"Generating {outputFileName}.{options.outputFileExtension} to {options.outputDirectory}.");

                using StreamWriter file = new StreamWriter($"{options.outputDirectory}\\{outputFileName}.{options.outputFileExtension}");
                file.WriteLine(result);
            }

            Environment.ExitCode = (int)ExitCode.Success;
        }
        catch (Exception ex)
        {
            if (options.verbose)
            {
                Console.WriteLine($"An error has been encountered: {ex}");
            }
            Environment.ExitCode = (int)ExitCode.UnknownError;
        }
    }

    private static void RunAutomation(Options options, string inputFileName, string outputFileName = "")
    {
        try
        {
            var jsonInput = File.ReadAllText(inputFileName);
            var stringTemplate = File.ReadAllText(options.pattern);
            var template = Handlebars.Compile(stringTemplate);

            //var deserializedMapping = JsonConvert.DeserializeObject<VdwDataObjectMappings>(jsonInput);
            var deserializedMapping = JObject.Parse(jsonInput);

            var result = template(deserializedMapping);

            if (options.verbose)
            {
                Console.WriteLine(result);
            }

            if (options.output)
            {
                if (outputFileName == "")
                {
                    // Try to extract the name from the JSON metadata according to the Data Warehouse Automation schema
                    // The schema supports multiple structures:
                    // 1. Top-level "name" property (DataObjectMappingList or free-form objects)
                    // 2. "dataObjectMappings[0].name" property (when top-level name is not present)
                    try
                    {
                        // First, try to get the top-level name property
                        if (deserializedMapping["name"] != null)
                        {
                            outputFileName = (string)deserializedMapping["name"];
                        }
                        // If no top-level name, try to get the first mapping's name
                        else if (deserializedMapping["dataObjectMappings"]?[0]?["name"] != null)
                        {
                            outputFileName = (string)deserializedMapping["dataObjectMappings"][0]["name"];
                        }
                        else
                        {
                            // If neither exists, use filename + timestamp
                            throw new Exception("No name property found");
                        }
                    }
                    catch
                    {
                        outputFileName = Path.GetFileNameWithoutExtension(inputFileName) + '_' + DateTime.Now.ToString("yyyyMMddHHmmss");

                        if (options.verbose)
                        {
                            Console.WriteLine($"The standard 'name' property was not found in the expected locations, so the file name has been defaulted to {outputFileName}.");
                        }
                    }
                }

                Console.WriteLine($"Generating {outputFileName}.{options.outputFileExtension} to {options.outputDirectory}.");

                using StreamWriter file = new StreamWriter($"{options.outputDirectory}\\{outputFileName}.{options.outputFileExtension}");
                file.WriteLine(result);
            }

            Environment.ExitCode = (int)ExitCode.Success;
        }
        catch (Exception ex)
        {
            if (options.verbose)
            {
                Console.WriteLine($"An error has been encountered: {ex}");
            }
            Environment.ExitCode = (int)ExitCode.UnknownError;
        }
    }

    /// <summary>
    /// Application exit codes, returned to the command line via the main().
    /// </summary>
    enum ExitCode : int
    {
        Success = 0,
        NoFile = 1,
        InvalidFile = 2,
        NoPattern = 3,
        InvalidPattern = 4,
        UnknownError = 10
    }
}
