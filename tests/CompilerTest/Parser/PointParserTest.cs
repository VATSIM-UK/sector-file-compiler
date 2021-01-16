using Xunit;
using Compiler.Model;
using Compiler.Parser;

namespace CompilerTest.Parser
{
    public class PointParserTest
    {
        [Fact]
        public void TestItReturnsCoordinatePoints()
        {
            Coordinate expectedCoordinate = new Coordinate("N050.57.00.000", "W001.21.24.490");
            Point point = PointParser.Parse("N050.57.00.000", "W001.21.24.490");

            Assert.Equal(Point.TypeCoordinate, point.Type());
            Assert.Equal(expectedCoordinate, point.Coordinate);
        }

        [Fact]
        public void TestItReturnsInvalidOnIdentifierTooLong()
        {
            Point point = PointParser.Parse("ABCDEFG", "ABCDEFG");
            Assert.Equal(PointParser.invalidPoint, point);
        }

        [Fact]
        public void TestItReturnsInvalidOnIdentifiersNoMatch()
        {
            Point point = PointParser.Parse("ABCDE", "BCDEF");
            Assert.Equal(PointParser.invalidPoint, point);
        }

        [Fact]
        public void TestItReturnsIdentifierPoint()
        {
            Point point = PointParser.Parse("ABCDE", "ABCDE");

            Assert.Equal(Point.TypeIdentifier, point.Type());
            Assert.Equal("ABCDE ABCDE", point.ToString());
        }
    }
}
