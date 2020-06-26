using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Compiler.Validate
{
    public class AirportValidator
    {
        private static readonly string ICAO_REGEX = "^[A-Z]{4}$";

        private static readonly string ANY_AIRPORT_STRING = "000A";

        private static readonly string GUEST_AIRPORT_STRING = "*";

        public static bool IcaoValid(string code)
        {
            return Regex.IsMatch(code, AirportValidator.ICAO_REGEX);
        }

        /*
         * Also includes 000A for all adjacent airports.
         */
        public static bool ValidEuroscopeAirport(string code)
        {
            return code == AirportValidator.ANY_AIRPORT_STRING ||
                Regex.IsMatch(code, AirportValidator.ICAO_REGEX);
        }

        /*
         * Is the icao string valid for sector guests
         */
        public static bool ValidSectorGuestAirport(string code)
        {
            return code == AirportValidator.GUEST_AIRPORT_STRING ||
                Regex.IsMatch(code, AirportValidator.ICAO_REGEX);
        }
    }
}
