using Compiler.Model;

namespace Compiler.Parser
{
    public class CoordinateParser
    {
        private const char COORDINATE_SEPARATOR = '.';

        public static readonly Coordinate invalidCoordinate = new Coordinate("", "");
        
        public static Coordinate Parse(string latitude, string longitude)
        {
            string parsedLatitude = CoordinateParser.ParseCoordinate(true, latitude.Trim());
            string parsedLongitude = CoordinateParser.ParseCoordinate(false, longitude.Trim());

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

            if (
                !int.TryParse(parts[0].Substring(1), out _) ||
                !int.TryParse(parts[1], out _) ||
                !int.TryParse(parts[2], out _) ||
                !int.TryParse(parts[3], out _)
            ) {
                return null;
            }

            return coordinate;
        }
    }
}
