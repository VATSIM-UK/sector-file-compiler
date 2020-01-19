using Compiler.Output;
using Compiler.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Compiler.Argument
{
    public class CompilerArguments
    {
        public const string COMPILER_VERISON = "1.0.0";

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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        // The output file for the ESE
        public TextWriter OutFileEse { set; get; }

        // The output file for the SCT
        public TextWriter OutFileSct { set; get; }

        // The order in which ESE sections should be output
        public List<OutputSections> EseSections { get; set; } = new List<OutputSections>
        {
            OutputSections.ESE_HEADER,
            OutputSections.ESE_PREAMBLE,
            OutputSections.ESE_POSITIONS,
            OutputSections.ESE_FREETEXT,
            OutputSections.ESE_SIDSSTARS,
            OutputSections.ESE_AIRSPACE,
        };

        // The order in which SCT sections should be output
        public List<OutputSections> SctSections { get; set; } = new List<OutputSections>
        {
            OutputSections.SCT_HEADER,
            OutputSections.SCT_COLOUR_DEFS,
            OutputSections.SCT_INFO,
            OutputSections.SCT_AIRPORT,
            OutputSections.SCT_RUNWAY,
            OutputSections.SCT_VOR,
            OutputSections.SCT_NDB,
            OutputSections.SCT_FIXES,
            OutputSections.SCT_GEO,
            OutputSections.SCT_LOW_AIRWAY,
            OutputSections.SCT_HIGH_AIRWAY,
            OutputSections.SCT_ARTCC,
            OutputSections.SCT_ARTCC_HIGH,
            OutputSections.SCT_ARTCC_LOW,
            OutputSections.SCT_SID,
            OutputSections.SCT_STAR,
            OutputSections.SCT_LABELS,
            OutputSections.SCT_REGIONS
        };

        // Should we validate the file before output
        public bool ValidateOutput { set; get; } = true;

        // Should we strip comments out of the final output
        public bool StripComments { set; get; } = false;

        // Should we strip blank lines out of the final output
        public bool RemoveBlankLines { set; get; } = false;

        // Should we force all route segments (in SCT SID/STAR) to be joined up
        public bool EnforceContiguousRouteSegments { set; get; } = false;

        // Whether or not compilation should include a comment at the start of every input file
        public bool DisplayInputFiles { set; get; } = false;

        // The build version to use
        public string BuildVersion { set; get; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
