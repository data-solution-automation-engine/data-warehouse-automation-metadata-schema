using System;
using System.IO;
using HandlebarsDotNet;
using CommandLine;
using Newtonsoft.Json;
using DataWarehouseAutomation;

namespace RunDwhAutomation
{
    class Program
    {
        static int Main(string[] args)
        {
            // An optimistic start.
            Environment.ExitCode = (int)ExitCode.Success;

            #region unit testing
            // Unit testing only.
            //var testArgs = new string[]
            //{
            //  //   "-i", @"C:\Github\Data-Warehouse-Automation-Metadata-Schema\ClassLibrary\DataWarehouseAutomation\Sample_Metadata\sampleBasic.json"
            //     "-i", @"C:\Github\Data-Warehouse-Automation-Metadata-Schema\ClassLibrary\DataWarehouseAutomation\Sample_Metadata\"
            //    ,"-p", @"C:\Github\Data-Warehouse-Automation-Metadata-Schema\ClassLibrary\DataWarehouseAutomation\Sample_Templates\TemplateSampleBasic.Handlebars"
            //    ,"-o"
            //    ,"-d", @"C:\Files\"
            //    ,"-e", "sql"
            //    ,"-f", "roelant"
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
            #endregion

            CommandLineArgumentHelper environmentHelper = new CommandLineArgumentHelper();
            string[] localArgs = environmentHelper.args;

            Parser.Default.ParseArguments<Options>(localArgs).WithParsed(options =>
            {
                // Make sure there is a default directory set when output is enabled
                if (options.Output)
                {
                    if (options.OutputDirectory == null)
                    {
                        options.OutputDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    }

                    if (options.OutputFileExtension == null)
                    {
                        options.OutputFileExtension = "txt";
                    }
                }

                // Determine if the output is a file or path

                var path = Path.GetExtension(options.Input);
                bool IsPath;
                if (path == String.Empty)
                {
                    IsPath = true;
                }
                else
                {
                    IsPath = false;
                }

                // Managing verbose information back to user
                if (options.Verbose)
                {
                    Console.WriteLine("Verbose mode enabled.");

                    if (options.Input != null && options.Input.Length > 0)
                    {
                        Console.WriteLine($"The input (file or directory) provided is {options.Input}");
                        if (IsPath)
                        {
                            Console.WriteLine($"{options.Input} is evaluated as a directory.");
                        }
                        else
                        {
                            Console.WriteLine($"{options.Input} is evaluated as a file.");
                        }
                    }

                    if (options.Pattern != null && options.Pattern.Length > 0)
                    {
                        Console.WriteLine($"The pattern used is {options.Pattern}");
                    }

                    if (options.Output)
                    {
                        Console.WriteLine($"Output is enabled.");
                        Console.WriteLine($"The Output Directory is {options.OutputDirectory}.");
                        Console.WriteLine($"The File Exension for output file(s) is {options.OutputFileExtension}");
                    }  

                    //Console.WriteLine();
                }

                #region Core
                // Do the main stuff
                HandleBarsHelpers.RegisterHandleBarsHelpers();
                
                if (IsPath)
                {
                    var localFiles = Directory.GetFiles(options.Input, "*.json");

                    foreach (var file in localFiles)
                    {
                        if (options.OutputFileName == null)
                        {
                            RunAutomation(options, file, "");
                        }
                        else
                        {
                            RunAutomation(options, file, options.OutputFileName + Array.IndexOf(localFiles, file));
                        }
                    }
                }
                else
                {
                    if (options.OutputFileName == null)
                    {
                        RunAutomation(options, options.Input, "");
                    }
                    else
                    {
                        RunAutomation(options, options.Input, options.OutputFileName );
                    }                    
                }
                #endregion

            }).WithNotParsed(e => {
                Console.WriteLine($"An error was encountered while parsing the parameters/arguments. Please review the above possible options.");
            }); ;


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
                var stringTemplate = File.ReadAllText(options.Pattern);
                var template = Handlebars.Compile(stringTemplate);
                var deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);

                var result = template(deserialisedMapping);

                if (options.Verbose)
                {
                    Console.WriteLine(result);
                }

                if (options.Output)
                {
                    if (outputFileName == "")
                    {
                        outputFileName = deserialisedMapping.dataObjectMappings[0].mappingName;
                    }

                    Console.WriteLine($"Generating {outputFileName}.{options.OutputFileExtension} to {options.OutputDirectory}.");

                    using (StreamWriter file = new StreamWriter($"{options.OutputDirectory}\\{outputFileName}.{options.OutputFileExtension}"))
                    {
                        file.WriteLine(result);
                    }
                }

                Environment.ExitCode = (int)ExitCode.Success;
            }
            catch (Exception ex)
            {
                if (options.Verbose)
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
            public string Input { get; set; }

            [Option('p', "pattern", Required = true, HelpText = "The filename for the (input) Handlebars pattern.")]
            public string Pattern { get; set; }

            // Outputs
            [Option('o', "output", Required = false, HelpText = "Enable output to be spooled to disk (enable/disable) - default is disable.")]
            public bool Output { get; set; }

            [Option('d', "outputdirectory", Required = false, HelpText = "The directory where spool files (output) are placed. If not provided, the execution directory will be assumed.")]
            public string OutputDirectory { get; set; }

            [Option('e', "outputextension", Required = false, HelpText = "The extension used for the output file(s). This is defaulted to txt when left empty.")]
            public string OutputFileExtension { get; set; }

            [Option('f', "outputfilename", Required = false, HelpText = "The name of the output file(s). This is defaulted to the mapping name in the metadata object when left empty.")]
            public string OutputFileName { get; set; }

            // Other
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }
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
