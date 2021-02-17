using System.Text.RegularExpressions;

namespace Compiler.Validate
{
    public class AirportValidator
    {
        private static readonly string IcaoRegex = "^[A-Z]{4}$";

        private static readonly string AnyAirportString = "000A";

        private static readonly string GuestAirportString = "*";

        public static bool IcaoValid(string code)
        {
            return Regex.IsMatch(code, IcaoRegex);
        }

        /*
         * Also includes 000A for all adjacent airports.
         */
        public static bool ValidEuroscopeAirport(string code)
        {
            return code == AnyAirportString ||
                Regex.IsMatch(code, IcaoRegex);
        }

        /*
         * Is the icao string valid for sector guests
         */
        public static bool ValidSectorGuestAirport(string code)
        {
            return code == GuestAirportString ||
                Regex.IsMatch(code, IcaoRegex);
        }
    }
}
