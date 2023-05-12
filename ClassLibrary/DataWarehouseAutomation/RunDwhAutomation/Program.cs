using System;
using System.IO;
using HandlebarsDotNet;
using CommandLine;
using DataWarehouseAutomation;
using Newtonsoft.Json.Linq;

namespace RunDwhAutomation
{
    class Program
    {
        static int Main(string[] args)
        {
            // An optimistic start.
            Environment.ExitCode = (int)ExitCode.Success;

            // Get the handlebars extensions from the DataWarehouseAutomation class library.
            HandleBarsHelpers.RegisterHandleBarsHelpers();

            #region unit testing
            // Unit testing only.
            //var testArgs = new string[]
            //{    "-i", @"D:\Git_Repos\TEAM\TEAM\bin\Debug\Output\"
            //    ,"-p", @"D:\Git_Repos\Data_Warehouse_Automation_Metadata_Interface\ClassLibrary\DataWarehouseAutomation\Sample_Templates\TemplateSatelliteView.Handlebars"
            //    ,"-o"
            //    ,"-d", @"D:\Workspace"
            //    ,"-e", "sql"
            //    //,"-f", "roelant"
            //    ,"-v"
            //};

            //var testArgs = new string[]
            //{
            //       "-i", @"C:\Github\Data-Warehouse-Automation-Metadata-Schema\ClassLibrary\DataWarehouseAutomation\Sample_Metadata\sampleBasic.json"
            //    "-i", @"C:\Files\Test\"
            //    ,"-p", @"C:\Files\Test\TemplateSampleBasic.Handlebars"
            //    ,"-o"
            //    ,"-f", "roelant"
            //    ,"-v"
            //};

            //var testArgs = new string[]
            //{
            //     "-i", @"D:\RunDwhAutomation\Input\dbo.SAT_DeliveryTypeSalesControl_InforLN.json"
            //    ,"-p", @"D:\RunDwhAutomation\Pattern\test.handlebars"
            //    ,"-o"
            //    ,"-f", "tst"
            //    ,"-v"
            //};

            #endregion

            CommandLineArgumentHelper environmentHelper = new CommandLineArgumentHelper();
            //CommandLineArgumentHelper environmentHelper = new CommandLineArgumentHelper(testArgs);
            string[] localArgs = environmentHelper.args;

            Parser.Default.ParseArguments<Options>(localArgs).WithParsed(options =>
            {
                // Make sure there is a default directory set when output is enabled
                if (options.output)
                {
                    if (options.outputDirectory == null)
                    {
                        options.outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    }

                    if (options.outputFileExtension == null)
                    {
                        options.outputFileExtension = "txt";
                    }
                }

                // Determine if the output is a file or path

                var path = Path.GetExtension(options.input);
                bool isPath;
                isPath = path == String.Empty;

                // Managing verbose information back to user
                if (options.verbose)
                {
                    Console.WriteLine("Verbose mode enabled.");

                    if (!string.IsNullOrEmpty(options.input))
                    {
                        Console.WriteLine($"The input (file or directory) provided is {options.input}");

                        Console.WriteLine(isPath
                            ? $"{options.input} is evaluated as a directory."
                            : $"{options.input} is evaluated as a file.");
                    }

                    if (!string.IsNullOrEmpty(options.pattern))
                    {
                        Console.WriteLine($"The pattern used is {options.pattern}");
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
                //HandleBarsHelpers.RegisterHandleBarsHelpers();
                
                if (isPath)
                {
                    var localFiles = Directory.GetFiles(options.input, "*.json");

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
                else
                {
                    if (options.outputFileName == null)
                    {
                        RunAutomation(options, options.input);
                    }
                    else
                    {
                        RunAutomation(options, options.input, options.outputFileName );
                    }                    
                }
                #endregion

            }).WithNotParsed(e => {
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

        private static void RunAutomation(Options options, string inputFileName, string outputFileName = "")
        {
            try
            {
                var jsonInput = File.ReadAllText(inputFileName);
                var stringTemplate = File.ReadAllText(options.pattern);
                var template = Handlebars.Compile(stringTemplate);

                //var deserialisedMapping = JsonConvert.DeserializeObject<VdwDataObjectMappings>(jsonInput);
                var freeFormMapping = JObject.Parse(jsonInput);

                var result = template(freeFormMapping);

                if (options.verbose)
                {
                    Console.WriteLine(result);
                }

                if (options.output)
                {
                    if (outputFileName == "")
                    {
                        //outputFileName = deserialisedMapping.dataObjectMappings[0].mappingName; // you could read this from the free form mapping file, too
                        try
                        {
                            outputFileName = (string) freeFormMapping["dataObjectMappings"][0]["mappingName"]; 
                        }
                        catch
                        {
                            outputFileName = Path.GetFileNameWithoutExtension(inputFileName) + '_' +DateTime.Now.ToString("yyyyMMddHHmmss");

                            if (options.verbose)
                            {
                                Console.WriteLine($"The standard 'mappingName' segment was not found where expected, so the file name has been defaulted to {outputFileName}.");
                            }
                        }
                    }

                    Console.WriteLine($"Generating {outputFileName}.{options.outputFileExtension} to {options.outputDirectory}.");

                    using (StreamWriter file = new StreamWriter($"{options.outputDirectory}\\{outputFileName}.{options.outputFileExtension}"))
                    {
                        file.WriteLine(result);
                    }
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
        /// Command line options (arguments, parameters).
        /// </summary>
        class Options
        {
            // Inputs
            [Option('i', "input", Required = true, HelpText = "The filename or directory of the (input) Json file(s) containing the automation metadata.")]
            public string input { get; set; }

            [Option('p', "pattern", Required = true, HelpText = "The filename for the (input) Handlebars pattern.")]
            public string pattern { get; set; }

            // Outputs
            [Option('o', "output", Required = false, HelpText = "Enable output to be spooled to disk (enable/disable) - default is disable.")]
            public bool output { get; set; }

            [Option('d', "outputdirectory", Required = false, HelpText = "The directory where spool files (output) are placed. If not provided, the execution directory will be assumed.")]
            public string outputDirectory { get; set; }

            [Option('e', "outputextension", Required = false, HelpText = "The extension used for the output file(s). This is defaulted to txt when left empty.")]
            public string outputFileExtension { get; set; }

            [Option('f', "outputfilename", Required = false, HelpText = "The name of the output file(s). This is defaulted to the mapping name in the metadata object when left empty.")]
            public string outputFileName { get; set; }

            // Other
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool verbose { get; set; }
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

        /// <summary>
        /// Helper class to simplify providing unit-testing parameters/arguments.
        /// </summary>
        internal class CommandLineArgumentHelper
        {
            internal string[] args;

            public CommandLineArgumentHelper()
            {
                this.args = Environment.GetCommandLineArgs();
            }

            public CommandLineArgumentHelper(string [] testArgs)
            {
                this.args = testArgs;
            }
        }
    }
}
