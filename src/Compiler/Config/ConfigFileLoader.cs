using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using Compiler.Argument;
using Compiler.Exception;

namespace Compiler.Config
{
    public class ConfigFileLoader
    {
        private readonly ConfigIncludeLoader includeLoader;
        private readonly ConfigOptionsLoader optionsLoader;

        public ConfigFileLoader(
            ConfigIncludeLoader includeLoader,
            ConfigOptionsLoader optionsLoader
        )
        {
            this.includeLoader = includeLoader;
            this.optionsLoader = optionsLoader;
        }
        
        /**
         * Load config files and append roots as required.
         */
        public ConfigInclusionRules LoadConfigFiles(List<string> files, CompilerArguments arguments)
        {
            ConfigInclusionRules config = new ConfigInclusionRules();

            foreach (string file in files)
            {
                // Get the full path to the config file
                string fullPath = Path.GetFullPath(file);

                // Parse the config file as JSON
                JObject jsonConfig;
                try
                {
                    jsonConfig = JObject.Parse(File.ReadAllText(fullPath));
                }
                catch (Newtonsoft.Json.JsonReaderException e)
                {
                    throw new ConfigFileInvalidException("Invalid JSON in " + fullPath + ": " + e.Message);
                }
                catch (FileNotFoundException)
                {
                    throw new ConfigFileInvalidException("Config file not found");
                }

                optionsLoader.LoadOptions(arguments, jsonConfig, fullPath);
                includeLoader.LoadConfig(config, jsonConfig, fullPath);
            }
            
            return config;
        }
    }
}
