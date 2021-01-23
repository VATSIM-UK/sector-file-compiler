using Compiler.Config;

namespace Compiler.Output
{
    /*
     * Generates keys for output groups, depending on their input data type
     * and in the case of airports, their icao codes.
     */
    public class OutputGroupKeyGenerator
    {
        /*
         * Airport keys are special, in that some of the data should not be grouped by airfield, just lumped in together.
         */
        public static string GenerateAirportKey(ConfigFileSection configFileSection, string airport)
        {
            return configFileSection.OutputGroupDescriptor == null 
                ? $"airport.{configFileSection.DataType}"
                : $"airport.{configFileSection.DataType}.{airport}";
        }

        public static string GeneratEnrouteKey(ConfigFileSection configFileSection)
        {
            return $"enroute.{configFileSection.DataType}";
        }

        public static string GenerateMiscKey(ConfigFileSection configFileSection)
        {
            return $"misc.{configFileSection.DataType}";
        }
    }
}
