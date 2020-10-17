using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Input;
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
            return String.Format("Start {0} {1}", airport, configSection.OutputGroupDescriptor);
        }

        public static string GeneratEnrouteDescription(ConfigFileSection configSection)
        {
            return String.Format("Start enroute {0}", configSection.OutputGroupDescriptor);
        }

        public static string GenerateMiscDescription(ConfigFileSection configSection)
        {
            return String.Format("Start misc {0}", configSection.OutputGroupDescriptor);
        }
    }
}
