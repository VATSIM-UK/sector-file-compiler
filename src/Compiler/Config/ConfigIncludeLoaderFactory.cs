using Compiler.Argument;

namespace Compiler.Config
{
    public static class ConfigIncludeLoaderFactory
    {
        public static ConfigIncludeLoader Make(CompilerArguments arguments)
        {
            return new(
                new FolderInclusionRuleLoaderFactory(arguments),
                new FileInclusionRuleLoaderFactory()
            );
        }
    }
}
