
using Compiler.Output;

namespace Compiler.Config
{
    public class FileInclusionRuleLoaderFactory
    {
        public FileInclusionRuleLoader Create(
            ConfigPath path,
            OutputGroup outputGroup,
            string configFilePath
        ) {
            return new(path, outputGroup, new (configFilePath));
        }
    }
}
