using Compiler.Model;

namespace Compiler.Parser
{
    public class CoordinateParser
    {
        private const char COORDINATE_SEPARATOR = '.';

        public static readonly Coordinate invalidCoordinate = new Coordinate("", "");
        
        public static Coordinate Parse(string latitude, string longitude)
        {
            string parsedLatitude = CoordinateParser.ParseCoordinate(true, latitude);
            string parsedLongitude = CoordinateParser.ParseCoordinate(false, longitude);

            if (parsedLatitude == null || parsedLongitude == null)
            {
                return CoordinateParser.invalidCoordinate;
            }

            return new Coordinate(parsedLatitude, parsedLongitude);
        }

        private static string ParseCoordinate(bool latitude, string coordinate)
        {
            string[] parts = coordinate.Split(CoordinateParser.COORDINATE_SEPARATOR);

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

            int value = 0;
            if (
                !int.TryParse(parts[0].Substring(1), out value) ||
                !int.TryParse(parts[1], out value) ||
                !int.TryParse(parts[2], out value) ||
                !int.TryParse(parts[3], out value)
            ) {
                return null;
            }

            return coordinate;
        }
    }
}
