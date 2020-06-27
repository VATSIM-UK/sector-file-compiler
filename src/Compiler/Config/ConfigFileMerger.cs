using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Argument;
using Compiler.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Compiler.Config
{
    public class ConfigFileMerger
    {
        public static JObject MergeConfigFiles(CompilerArguments args)
        {
            // Iterate the config files, load them and merge them
            JObject mergedConfig = new JObject();
            foreach (IFileInterface configFile in args.ConfigFiles)
            {
                JObject loadedConfig = ConfigFileLoader.LoadConfigFile(configFile);

                foreach (var item in mergedConfig)
                {
                    if (loadedConfig.ContainsKey(item.Key) && item.Value.Type != loadedConfig[item.Key].Type)
                    {
                        string errorMessage = String.Format(
                            "Incompatible configs at key {0}, cannot merge",
                            item.Key
                        );
                        throw new ArgumentException(errorMessage);
                    }
                }

                mergedConfig.Merge(
                    ConfigFileLoader.LoadConfigFile(configFile),
                    new JsonMergeSettings
                    {
                        MergeArrayHandling = MergeArrayHandling.Union
                    }
                );
            }

            return mergedConfig;
        }
    }
}
