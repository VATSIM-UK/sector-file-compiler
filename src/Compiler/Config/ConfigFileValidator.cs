using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Compiler.Config
{
    class ConfigFileValidator
    {
        public static String lastError { get; private set; }

        public static bool ConfigFileValid(JObject config)
        {
            foreach (var item in config)
            {
                if (item.Value.Type != JTokenType.Array)
                {
                    lastError = String.Format("Key %s is not an array", item.Key);
                    return false;
                }

                if (!ConfigFileSectionsMapper.ConfigSectionValid(item.Key.ToString()))
                {
                    lastError = String.Format("Key %s is not a valid config section", item.Key);
                    return false;
                }

                foreach (var inputFile in item.Value)
                {
                    if (inputFile.Type != JTokenType.String)
                    {
                        lastError = String.Format("Value %s is not a valid string", item.Value.ToString());
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
