using Compiler.Model;

namespace Compiler.Parser
{
    public class PointParser
    {
        public static readonly Point InvalidPoint = new("--INVALID--");

        /**
         * Parses "points" on an ARTCC Line or similar. Will try coordinates and
         * if not valid, treat the point as an identifier.
         */
        public static Point Parse(string point1, string point2)
        {
            Coordinate coordinate = CoordinateParser.Parse(point1, point2);
            if (!coordinate.Equals(CoordinateParser.InvalidCoordinate))
            {
                return new Point(coordinate);
            }

            if (point1 != point2)
            {
                return PointParser.InvalidPoint;
            }

            if (point1.Length > 6 || point2.Length > 6)
            {
                return PointParser.InvalidPoint;
            }

            return new Point(point1);
        }
    }
}
