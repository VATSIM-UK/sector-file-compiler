using System;
using System.Collections.Generic;
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
                if (
                    item.Key == ConfigFileSectionsMapper.GetConfigSectionForOutputSection(OutputSections.ESE_AIRSPACE) &&
                    !ValidateEseAirspace(item)
                ) {
                    return false;
                } else if (!ValidateDefault(item)) {
                    return false;
                }
            }

            return true;
        }

        private static bool ValidateDefault(KeyValuePair<string, JToken> config)
        {
            if (config.Value.Type != JTokenType.Array)
            {
                lastError = String.Format("Key {0} is not an array", config.Key);
                return false;
            }

            if (!ConfigFileSectionsMapper.ConfigSectionValid(config.Key.ToString()))
            {
                lastError = String.Format("Key {0} is not a valid config section", config.Key);
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

        /*
         * This is a special section with some defined subsections.
         */
        private static bool ValidateEseAirspace(KeyValuePair<string, JToken> config)
        {
            if (config.Value.Type != JTokenType.Object)
            {
                lastError = String.Format("Key {0} is not an object", config.Key);
                return false;
            }

            JObject configObject = JObject.Parse(config.Value.ToString());
            foreach (var configToken in configObject)
            {
                if (
                    !SubsectionMapper.IsValidSubsectionForSection(
                        OutputSections.ESE_AIRSPACE,
                        ConfigFileSectionsMapper.GetSubsectionForConfigSubsection(configToken.Key)
                    )
                ) {
                    lastError = String.Format("Key {0} is not a valid subsection for ESE AIRSPACE", "test");
                    return false;
                }

                if (!ValidateDefault(configToken))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
