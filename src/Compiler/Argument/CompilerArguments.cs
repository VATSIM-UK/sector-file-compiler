using Compiler.Output;
using System;

namespace Compiler.Argument
{
    public class CompilerArguments
    {
        // The place to find the configuration JSON file.
        private string configFilePath;

        public string ConfigFilePath
        {
            get
            {
                return this.configFilePath;
            }

            set
            {
                this.configFilePath = value;
            }
        }

        // The output verbosity for the compiler
        private OutputVerbosity verbosity;

        public OutputVerbosity Verbosity
        {
            get
            {
                return this.verbosity == default(OutputVerbosity) ? OutputVerbosity.Debug : this.verbosity;
            }
            set
            {
                this.verbosity = value;
            }
        }

        public override string ToString()
        {
            string output = "";
            output += "Config File Path: " + this.ConfigFilePath + Environment.NewLine;
            output += "Output Verbosity: " + this.verbosity.ToString() + Environment.NewLine;
            return output;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            CompilerArguments compare = (CompilerArguments)obj;

            return this.configFilePath == compare.configFilePath
                && this.verbosity == compare.verbosity;
        }
    }
}
