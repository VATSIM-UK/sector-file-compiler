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
        public static Dictionary<string, List<string>> MergeConfigFiles(CompilerArguments args)
        {
            JObject mergedConfig = new JObject();

            // Iterate the config files, load them and merge them
            foreach (IFileInterface configFile in args.ConfigFiles)
            {
                mergedConfig.Merge(
                    ConfigFileLoader.LoadConfigFile(configFile),
                    new JsonMergeSettings {
                        MergeArrayHandling = MergeArrayHandling.Union
                    }
                );
            }

            // Return it in the format we want
            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(
                mergedConfig.ToString()
            );
        }
    }
}
