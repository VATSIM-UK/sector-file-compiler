using Compiler.Output;
using System;
using System.Collections.Generic;
using Compiler.Transformer;

namespace Compiler.Argument
{
    public class CompilerArguments
    {
        public const int EmptyFolderIgnore = 0;
        public const int EmptyFolderWarning = 1;
        public const int EmptyFolderError = 2;

        public const string DefaultBuildVersion = "BUILD_VERSION";

        public List<string> ConfigFiles { get; } = new();

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || GetType() != obj.GetType())
            {
                return false;
            }

            CompilerArguments compare = (CompilerArguments)obj;

            // Both have nothing, so equal
            if (ConfigFiles.Count == 0 && compare.ConfigFiles.Count == 0)
            {
                return true;
            }

            // Different length, so definitely not equal
            if (ConfigFiles.Count != compare.ConfigFiles.Count)
            {
                return false;
            }

            // Check every one is equal.
            for (int i = 0; i < ConfigFiles.Count; i++)
            {
                if (!ConfigFiles[i].Equals(compare.ConfigFiles[i]))
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
        public List<AbstractOutputFile> OutputFiles { get; } = new();

        // Should we validate the file before output
        public bool ValidateOutput { set; get; } = true;

        // Should we strip comments out of the final output
        public bool StripComments { set; get; }

        // Replace tokens in the output
        public List<ITokenReplacer> TokenReplacers { get; } = new();

        // The version being built
        public string BuildVersion { get; set; } = DefaultBuildVersion;

        // What to do about empty folders
        public int EmptyFolderAction { get; set; } = EmptyFolderIgnore;

        // What mode should we run in.
        public RunMode Mode { get; set; } = RunMode.COMPILE;
    }
}
