using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using Compiler.Argument;
using Compiler.Exception;

namespace Compiler.Config
{
    public class ConfigFileLoader
    {
        private readonly IEnumerable<IConfigLoader> loaders;

        public ConfigFileLoader(IEnumerable<IConfigLoader> loaders) 
        {
            this.loaders = loaders;
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

                foreach (var loader in loaders)
                {
                    loader.LoadConfig(arguments, config, jsonConfig, file);
                }
            }
            
            return config;
        }
    }
}
