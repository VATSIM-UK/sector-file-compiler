using Compiler.Output;
using Compiler.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Compiler.Argument
{
    public class CompilerArguments
    {
        public IFileInterface ConfigFile { set; get; }

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

            return ((this.ConfigFile == null && compare.ConfigFile == null) || this.ConfigFile.Equals(compare.ConfigFile));
        }

        public TextWriter OutFile { set; get; }

        // The order in which ESE sections should be output
        public List<OutputSections> EseSections { get; set; } = new List<OutputSections>
        {
            OutputSections.ESE_PREAMBLE,
            OutputSections.ESE_POSITIONS,
            OutputSections.ESE_FREETEXT,
            OutputSections.ESE_SIDSSTARS,
            OutputSections.ESE_AIRSPACE,
        };

        // Should we validate the file before output
        public bool ValidateOutput { set; get; } = true;

    }
}
