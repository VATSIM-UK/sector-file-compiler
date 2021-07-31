
using Compiler.Output;

namespace Compiler.Config
{
    public static class FileInclusionRuleLoaderFactory
    {
        public static FileInclusionRuleLoader Create(
            ConfigPath path,
            OutputGroup outputGroup,
            string configFilePath
        ) {
            return new(path, outputGroup, new (configFilePath));
        }
    }
}
