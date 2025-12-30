using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunDwhAutomation
{
    /// <summary>
    /// Command line options (arguments, parameters).
    /// </summary>
    internal class Options
    {        // Inputs
        [Option('i', "input", Required = true, Separator = ';', HelpText = "The filename or directories (separated by semicolons) of the (input) Json file(s) containing the automation metadata.")]
        public IEnumerable<string> input { get; set; }

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

        [Option('r', "recursive", Required = false, HelpText = "Enable recursive search through subdirectories when input is a directory.")]
        public bool recursive { get; set; }

        [Option('c', "combine", Required = false, HelpText = "Combine all metadata files into a single object and process with template once.")]
        public bool combine { get; set; }
    }
}
