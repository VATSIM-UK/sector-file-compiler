
using Compiler.Output;

namespace Compiler.Config
{
    public static class FolderInclusionRuleLoaderFactory
    {
        public static FolderInclusionRuleLoader Create(
            ConfigPath path,
            OutputGroup outputGroup,
            string configFilePath
        ) {
            return new(path, outputGroup, new (configFilePath));
        }
    }
}
