using System.Collections.Generic;
using Compiler.Argument;
using Compiler.Exception;
using Newtonsoft.Json.Linq;

namespace Compiler.Config
{
    public class ConfigOptionsLoader
    {
        private readonly IEnumerable<IConfigOptionLoader> loaders;

        public ConfigOptionsLoader(IEnumerable<IConfigOptionLoader> loaders)
        {
            this.loaders = loaders;
        }

        public void LoadOptions(CompilerArguments arguments, JObject config, string configFile)
        {
            JToken optionsObject = config.SelectToken("options");
            if (optionsObject == null)
            {
                return;
            }

            if (optionsObject.Type != JTokenType.Object)
            {
                throw new ConfigFileInvalidException($"Config options must be an object in file {configFile}");
            }
            
            foreach (var loader in loaders)
            {
                loader.LoadConfig(arguments, (JObject) optionsObject, configFile);
            }
        }
    }
}