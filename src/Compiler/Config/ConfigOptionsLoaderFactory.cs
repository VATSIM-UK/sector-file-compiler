namespace Compiler.Config
{
    public static class ConfigOptionsLoaderFactory
    {
        public static ConfigOptionsLoader Make()
        {
            return new ConfigOptionsLoader(
                new IConfigOptionLoader[]
                {
                    new ConfigOutputFilesOptionLoader(),
                    new ConfigTokenReplaceOptionLoader(),
                    new EmptyFileOptionLoader()
                }
            );
        }
    }
}
