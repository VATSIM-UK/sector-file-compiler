using System.Collections.Generic;

namespace Compiler.Config
{
    public class ConfigFileLoaderFactory
    {
        public static ConfigFileLoader Make()
        {
            return new ConfigFileLoader(
                new List<IConfigLoader>{new ConfigIncludeLoader()}
            );
        }
    }
}