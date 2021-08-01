using Compiler.Argument;

namespace Compiler.Config
{
    public class ConfigFileLoaderFactory
    {
        public static ConfigFileLoader Make(CompilerArguments arguments)
        {
            return new(
                ConfigIncludeLoaderFactory.Make(arguments), 
                ConfigOptionsLoaderFactory.Make()
            );
        }
    }
}
