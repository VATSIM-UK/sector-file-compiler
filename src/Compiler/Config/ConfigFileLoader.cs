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
                // Parse the config file as JSON
                JObject jsonConfig;
                try
                {
                    jsonConfig = JObject.Parse(File.ReadAllText(file));
                }
                catch (Newtonsoft.Json.JsonReaderException e)
                {
                    throw new ConfigFileInvalidException("Invalid JSON in " + file + ": " + e.Message);
                }
                catch (FileNotFoundException)
                {
                    throw new ConfigFileInvalidException("Config file not found");
                }

                includeLoader.LoadConfig(config, jsonConfig, file);
                optionsLoader.LoadOptions(arguments, jsonConfig, file);
            }
            
            return config;
        }
    }
}
