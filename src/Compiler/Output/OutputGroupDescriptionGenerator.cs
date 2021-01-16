using Compiler.Config;

namespace Compiler.Output
{
    /*
     * Generates descriptions for output groups, depending on their input data type
     * and in the case of airports, their icao codes.
     */
    public class OutputGroupDescriptionGenerator
    {
        public static string GenerateAirportDescription(ConfigFileSection configSection, string airport)
        {
            return $"Start {airport} {configSection.OutputGroupDescriptor}";
        }

        public static string GeneratEnrouteDescription(ConfigFileSection configSection)
        {
            return $"Start enroute {configSection.OutputGroupDescriptor}";
        }

        public static string GenerateMiscDescription(ConfigFileSection configSection)
        {
            return $"Start misc {configSection.OutputGroupDescriptor}";
        }
    }
}
