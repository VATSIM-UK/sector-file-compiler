using Xunit;
using Compiler.Parser;
using Compiler.Model;

namespace CompilerTest.Parser
{
    public class CoordinateParserTest
    {
        [Fact]
        public void TestItParsesCoordinates()
        {
            Coordinate coordinate = CoordinateParser.Parse("N054.39.27.000", "W006.12.57.000");
            Assert.Equal("N054.39.27.000", coordinate.latitude);
            Assert.Equal("W006.12.57.000", coordinate.longitude);
        }

        [Fact]
        public void TestItReturnsInvalidOnInvalidLatitudeNorthSouth()
        {
            Coordinate coordinate = CoordinateParser.Parse("W054.39.27.000", "W006.12.57.000");
            Assert.Equal(CoordinateParser.invalidCoordinate, coordinate);
        }

        [Fact]
        public void TestItReturnsInvalidOnInvalidLatitudeParts()
        {
            Coordinate coordinate = CoordinateParser.Parse("N054.39.27", "W006.12.57.000");
            Assert.Equal(CoordinateParser.invalidCoordinate, coordinate);
        }

        [Fact]
        public void TestItReturnsInvalidOnInvalidLatitudeFirstPart()
        {
            Coordinate coordinate = CoordinateParser.Parse("N05a.39.27.000", "W006.12.57.000");
            Assert.Equal(CoordinateParser.invalidCoordinate, coordinate);
        }

        [Fact]
        public void TestItReturnsInvalidOnInvalidLatitudeSecondPart()
        {
            Coordinate coordinate = CoordinateParser.Parse("N054.39a.27.000", "W006.12.57.000");
            Assert.Equal(CoordinateParser.invalidCoordinate, coordinate);
        }

        [Fact]
        public void TestItReturnsInvalidOnInvalidLatitudeThirdPart()
        {
            Coordinate coordinate = CoordinateParser.Parse("N054.39.27a.000", "W006.12.57.000");
            Assert.Equal(CoordinateParser.invalidCoordinate, coordinate);
        }

        [Fact]
        public void TestItReturnsInvalidOnInvalidLatitudeFourthPart()
        {
            Coordinate coordinate = CoordinateParser.Parse("N054.39.27.a2", "W006.12.57.000");
            Assert.Equal(CoordinateParser.invalidCoordinate, coordinate);
        }

        [Fact]
        public void TestItReturnsInvalidOnInvalidLongitudeNorthSouth()
        {
            Coordinate coordinate = CoordinateParser.Parse("N054.39.27.000", "S006.12.57.000");
            Assert.Equal(CoordinateParser.invalidCoordinate, coordinate);
        }

        [Fact]
        public void TestItReturnsInvalidOnInvalidLongitudeParts()
        {
            Coordinate coordinate = CoordinateParser.Parse("N054.39.27.000", "W006.57.000");
            Assert.Equal(CoordinateParser.invalidCoordinate, coordinate);
        }

        [Fact]
        public void TestItReturnsInvalidOnInvalidLongitudeFirstCoordinate()
        {
            Coordinate coordinate = CoordinateParser.Parse("N054.39.27.000", "W0a6.12.57.000");
            Assert.Equal(CoordinateParser.invalidCoordinate, coordinate);
        }

        [Fact]
        public void TestItReturnsInvalidOnInvalidLongitudeSecondCoordinate()
        {
            Coordinate coordinate = CoordinateParser.Parse("N054.39.27.000", "W006.12a.57.000");
            Assert.Equal(CoordinateParser.invalidCoordinate, coordinate);
        }

        [Fact]
        public void TestItReturnsInvalidOnInvalidLongitudeThirdCoordinate()
        {
            Coordinate coordinate = CoordinateParser.Parse("N054.39.27.000", "W006.12.57a.000");
            Assert.Equal(CoordinateParser.invalidCoordinate, coordinate);
        }

        [Fact]
        public void TestItReturnsInvalidOnInvalidLongitudeFourthCoordinate()
        {
            Coordinate coordinate = CoordinateParser.Parse("N054.39.27.000", "W006.12.57.0a0");
            Assert.Equal(CoordinateParser.invalidCoordinate, coordinate);
        }
    }
}
