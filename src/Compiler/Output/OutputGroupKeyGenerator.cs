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
                ? string.Format("airport.{0}", configFileSection.DataType.ToString())
                : string.Format("airport.{0}.{1}", airport, configFileSection.DataType.ToString());
        }

        public static string GeneratEnrouteKey(ConfigFileSection configFileSection)
        {
            return string.Format("enroute.{0}", configFileSection.DataType.ToString());
        }

        public static string GenerateMiscKey(ConfigFileSection configFileSection)
        {
            return string.Format("misc.{0}", configFileSection.DataType.ToString());
        }
    }
}
