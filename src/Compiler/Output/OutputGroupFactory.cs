using Compiler.Config;

namespace Compiler.Output
{
    public class OutputGroupFactory
    {
        public static OutputGroup CreateAirport(ConfigFileSection configSection, string airport = "")
        {
            return configSection.OutputGroupDescriptor == null
                ? CreateMassGroup(OutputGroupKeyGenerator.GenerateAirportKey(configSection, airport))
                : CreateSpecificGroup(
                    OutputGroupKeyGenerator.GenerateAirportKey(configSection, airport),
                    OutputGroupDescriptionGenerator.GenerateAirportDescription(configSection, airport)
                );
        }

        public static OutputGroup CreateMisc(ConfigFileSection configSection)
        {
            return configSection.OutputGroupDescriptor == null
                ? CreateMassGroup(OutputGroupKeyGenerator.GenerateMiscKey(configSection))
                : CreateSpecificGroup(
                    OutputGroupKeyGenerator.GenerateMiscKey(configSection),
                    OutputGroupDescriptionGenerator.GenerateMiscDescription(configSection)
                );
        }

        public static OutputGroup CreateEnroute(ConfigFileSection configSection)
        {
            return configSection.OutputGroupDescriptor == null
                ? CreateMassGroup(OutputGroupKeyGenerator.GeneratEnrouteKey(configSection))
                : CreateSpecificGroup(
                    OutputGroupKeyGenerator.GeneratEnrouteKey(configSection),
                    OutputGroupDescriptionGenerator.GeneratEnrouteDescription(configSection)
                );
        }

        private static OutputGroup CreateMassGroup(string key)
        {
            return new OutputGroup(key);
        }

        private static OutputGroup CreateSpecificGroup(string key, string header)
        {
            return new OutputGroup(key, header);
        }
    }
}
