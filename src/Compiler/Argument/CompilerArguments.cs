using Compiler.Output;
using System;
using System.Collections.Generic;

namespace Compiler.Argument
{
    public class CompilerArguments
    {
        public const string COMPILER_VERISON = "1.0.0";

        public List<string> ConfigFiles { get; } = new List<string>();

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            CompilerArguments compare = (CompilerArguments)obj;

            // Both have nothing, so equal
            if (this.ConfigFiles.Count == 0 && compare.ConfigFiles.Count == 0)
            {
                return true;
            }

            // Different length, so definitely not equal
            if (this.ConfigFiles.Count != compare.ConfigFiles.Count)
            {
                return false ;
            }

            // Check every one is equal.
            for (int i = 0; i < this.ConfigFiles.Count; i++)
            {
                if (!this.ConfigFiles[i].Equals(compare.ConfigFiles[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        // All the output files that need to be created
        public List<AbstractOutputFile> OutputFiles { get; } = new List<AbstractOutputFile>();

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
