using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Compiler.Output;
using Newtonsoft.Json.Linq;

namespace Compiler.Config
{
    public class ConfigFileValidator
    {
        public static String lastError { get; private set; } = "No errors";

        public static bool ConfigFileValid(JObject config)
        {
            foreach (var item in config)
            {

                if (!ConfigFileSectionsMapper.ConfigSectionValid(item.Key.ToString()))
                {
                    lastError = String.Format("Key {0} is not a valid config section", item.Key);
                    return false;
                } else if (item.Value.Type != JTokenType.Array && item.Value.Type != JTokenType.Object)
                {
                    lastError = String.Format(
                        "Key {0} must be an array or object, {1} detected",
                        item.Key,
                        item.Value.Type.ToString()
                    );
                    return false;
                } else if (
                  item.Value.Type == JTokenType.Array &&
                  !ValidateFileArray(item)
                ) {
                    return false;
                } else if (
                  item.Value.Type == JTokenType.Object &&
                  !ValidateSubsection((JObject)item.Value)
                ) {
                    return false;
                }
            }

            return true;
        }

        private static bool ValidateFileArray(KeyValuePair<string, JToken> config)
        {
            if (config.Value.Type != JTokenType.Array)
            {
                lastError = String.Format("Key {0} is not an array", config.Key);
                return false;
            }

            foreach (var inputFile in config.Value)
            {
                if (inputFile.Type != JTokenType.String)
                {
                    lastError = String.Format("Value {0} is not a valid string", inputFile.ToString());
                    return false;
                }
            }

            return true;
        }

        private static bool ValidateSubsection(JObject config)
        {
            foreach (var subsection in config)
            {
                if (!ValidateFileArray(subsection))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
