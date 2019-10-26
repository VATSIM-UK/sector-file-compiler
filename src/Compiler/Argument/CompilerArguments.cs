using Compiler.Output;
using Compiler.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Compiler.Argument
{
    public class CompilerArguments
    {
        // The config file itself
        private IFileInterface configFile;
        public IFileInterface ConfigFile
        {
            set
            {
                this.configFile = value;
            }
            get
            {
                return this.configFile;
            }
        }

        public override string ToString()
        {
            string output = "";
            output += "Config File Path: " + this.ConfigFile.GetPath() + Environment.NewLine;
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

            return ((this.configFile == null && compare.configFile == null) || this.configFile.Equals(compare.configFile));
        }

        // The output file
        private TextWriter outFile;
        public TextWriter OutFile
        {
            set
            {
                this.outFile = value;
            }
            get
            {
                return this.outFile;
            }
        }

        // The order in which ESE sections should be output
        public List<OutputSections> EseSections { get; set; } = new List<OutputSections>
        {
            OutputSections.ESE_PREAMBLE,
            OutputSections.ESE_POSITIONS,
            OutputSections.ESE_FREETEXT,
            OutputSections.ESE_SIDSSTARS,
            OutputSections.ESE_AIRSPACE,
        };
    }
}
