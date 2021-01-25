using Compiler.Model;

namespace Compiler.Parser
{
    public static class CoordinateParser
    {
        private const char CoordinateSeparator = '.';

        public static readonly Coordinate InvalidCoordinate = new("", "");

        public static readonly Coordinate StarterCoordinate = new("S999.00.00.000", "E999.00.00.000");
        
        public static Coordinate Parse(string latitude, string longitude)
        {
            // If its the special value used in the sectorfile, allow it
            if (latitude == "S999.00.00.000" && longitude == "E999.00.00.000")
            {
                return StarterCoordinate;
            }

            string parsedLatitude = ParseCoordinate(true, latitude.Trim());
            string parsedLongitude = ParseCoordinate(false, longitude.Trim());

            if (parsedLatitude == null || parsedLongitude == null)
            {
                return InvalidCoordinate;
            }

            return new Coordinate(parsedLatitude, parsedLongitude);
        }

        public static bool TryParse(string latitude, string longitude, out Coordinate coordinate)
        {
            coordinate = Parse(latitude, longitude);
            return !coordinate.Equals(InvalidCoordinate);
        }

        private static string ParseCoordinate(bool latitude, string coordinate)
        {
            string[] parts = coordinate.Split(CoordinateSeparator);

            if (parts.Length != 4)
            {
                return null;
            }

            if (latitude && !parts[0].StartsWith('N') && !parts[0].StartsWith('S'))
            {
                return null;
            }

            if (!latitude && !parts[0].StartsWith('E') && !parts[0].StartsWith('W'))
            {
                return null;
            }

            if (
                !int.TryParse(parts[0].Substring(1), out int degrees) ||
                !int.TryParse(parts[1], out int minutes) ||
                !float.TryParse(parts[2] + "." + parts[3], out float seconds)
            ) {
                return null;
            }


            return (latitude && ValidateLatitude(degrees, minutes, seconds)) ||
                    (!latitude && ValidateLongitude(degrees, minutes, seconds))
                ? coordinate
                : null;
        }

        private static bool ValidateMinutesSeconds(int minutes, float seconds)
        {
            return minutes <= 60 && seconds <= 60.0;
        }

        private static bool ValidateLongitude(int degrees, int minutes, float seconds)
        {
            if ((degrees == 180 && (minutes != 0 || seconds != 0.0)) || degrees > 180) {
                return false;
            }

            return ValidateMinutesSeconds(minutes, seconds);
        }

        private static bool ValidateLatitude(int degrees, int minutes, float seconds)
        {
            if ((degrees == 90 && (minutes != 0 || seconds != 0.0)) || degrees > 90)
            {
                return false;
            }

            return ValidateMinutesSeconds(minutes, seconds);
        }
    }
}
