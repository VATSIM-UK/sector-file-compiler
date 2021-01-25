using Xunit;
using System.Collections.Generic;
using Compiler.Parser;
using Compiler.Model;

namespace CompilerTest.Parser
{
    public class CoordinateParserTest
    {
        [Theory]
        [InlineData("N054.39.27.000", "W006.12.57.000")]
        [InlineData("S999.00.00.000", "E999.00.00.000")] // Off-screen coordinate
        [InlineData("N054.39.60.000", "W006.12.60.000")] // 60 seconds
        [InlineData("N054.60.44.000", "W006.60.23.000")] // 60 minutes
        public void TestItParsesValidCoordinates(string latitude, string longitude)
        {
            Assert.Equal(new Coordinate(latitude, longitude), CoordinateParser.Parse(latitude, longitude));
        }

        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] {"W054.39.27.000", "W006.12.57.000"}, // Invalid latitude north south
            new object[] {"N054.39.27", "W006.12.57.000"}, // Invalid latitude parts
            new object[] {"N05a.39.27.000", "W006.12.57.000"}, // Invalid latitude degrees
            new object[] {"N054.39a.27.000", "W006.12.57.000"}, // Invalid latitude minutes
            new object[] {"N054.39.27a.000", "W006.12.57.000"}, // Invalid latitude seconds
            new object[] {"N054.39.27.a2", "W006.12.57.000"}, // Invalid latitude fractions
            new object[] {"N054.39.27.000", "S006.12.57.000"}, // Invalid longitude north south
            new object[] {"S054.39.27.000", "N006.12.57.000"}, // Invalid longitude north south
            new object[] {"N054.39.27.000", "W006.57.000"}, // Invalid longitude parts
            new object[] {"N054.39.27.000", "W0a6.12.57.000"}, // Invalid longitude degrees
            new object[] {"N054.39.27.000", "W006.12a.57.000"}, // Invalid longitude minutes
            new object[] {"N054.39.27.000", "W006.12.57a.000"}, // Invalid longitude seconds
            new object[] {"N054.39.27.000", "W006.12.57.0a0"}, // Invalid longitude fractions
            new object[] {"N090.00.00.001", "W006.12.57.000"}, // 90 degrees latitude, with fractions
            new object[] {"N090.00.01.000", "W006.12.57.000"}, // 90 degrees latitude, with seconds
            new object[] {"N090.01.00.000", "W006.12.57.000"}, // 90 degrees latitude, with minutes
            new object[] {"N091.00.00.000", "W006.12.57.000"}, // More than 90 degrees latitude
            new object[] {"N070.00.60.001", "W006.12.57.000"}, // More than 60 seconds latitude
            new object[] {"N070.61.00.000", "W006.12.57.000"}, // More than 60 minutes latitude
            new object[] {"N054.39.27.000", "W180.00.00.001"}, // 180 degrees longitude, with fractions
            new object[] {"N054.39.27.000", "W180.00.01.000"}, // 180 degrees longitude, with seconds
            new object[] {"N054.39.27.000", "W180.01.00.000"}, // 180 degrees longitude, with minutes
            new object[] {"N054.39.27.000", "W181.00.00.000"}, // More than 180 degrees longitude
            new object[] {"N054.39.27.000", "W171.00.60.001"}, // More than 60 seconds longitude
            new object[] {"N054.39.27.000", "W171.61.00.000"}, // More than 60 minutes longitude

        };

        [Theory]
        [MemberData(nameof(BadData))]
        public void TestItReturnsInvalidOnBadData(string latitude, string longitude)
        {
            Coordinate coordinate = CoordinateParser.Parse(latitude, longitude);
            Assert.Equal(CoordinateParser.InvalidCoordinate, coordinate);
        }

        [Fact]
        public void TestItReturnsTrueOnValidTryParse()
        {
            Assert.True(
                CoordinateParser.TryParse("N054.39.27.000", "W006.12.57.000", out Coordinate returnedCoordinate)
            );
            Assert.Equal(new Coordinate("N054.39.27.000", "W006.12.57.000"), returnedCoordinate);
        }
        
        [Fact]
        public void TestItReturnsFalseOnInvalidTryParse()
        {
            Assert.False(
                CoordinateParser.TryParse("abd", "def", out Coordinate returnedCoordinate)
            );
            Assert.Equal(CoordinateParser.InvalidCoordinate, returnedCoordinate);
        }
    }
}
