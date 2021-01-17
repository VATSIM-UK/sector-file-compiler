namespace Compiler.Config
{
    public static class ConfigOptionsLoaderFactory
    {
        public static ConfigOptionsLoader Make()
        {
            return new ConfigOptionsLoader(
                new []
                {
                    new ConfigOutputFilesOptionLoader()
                }
            );
        }
    }
}