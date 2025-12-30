using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunDwhAutomation
{
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

        public CommandLineArgumentHelper(string[] testArgs)
        {
            this.args = testArgs;
        }
    }
}
