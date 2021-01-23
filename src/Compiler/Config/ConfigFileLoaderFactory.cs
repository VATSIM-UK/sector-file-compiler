namespace Compiler.Config
{
    public class ConfigFileLoaderFactory
    {
        public static ConfigFileLoader Make()
        {
            return new ConfigFileLoader(
                new ConfigIncludeLoader(),
                ConfigOptionsLoaderFactory.Make()
            );
        }
    }
}