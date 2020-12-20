using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Parser;
using Compiler.Error;
using Compiler.Model;
using Compiler.Event;
using Compiler.Input;
using Compiler.Output;

namespace CompilerTest.Parser
{
    public class AirportParserTest: AbstractParserTestCase
    {
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "EGHI",
                "Southampton; comment1",
                "N050.57.00.000 W001.21.24.490 ;comment2",
                "120.220 ;comment3",
                "Another line!"
            }}, // Too many lines
            new object[] { new List<string>{
                "EGHI",
                "Southampton; comment1",
                "N050.57.00.000 W001.21.24.490 ;comment2"
            }}, // Too few lines
            new object[] { new List<string>{
                "1233",
                "Southampton; comment1",
                "N050.57.00.000 W001.21.24.490 ;comment2",
                "120.220 ;comment3"
            }}, // Invalid icao
            new object[] { new List<string>{
                "EGHI",
                "Southampton; comment1",
                "N050.57.00.000W001.21.24.490 ;comment2",
                "120.220 ;comment3"
            }}, // Invalid coordinate format
            new object[] { new List<string>{
                "EGHI",
                "Southampton; comment1",
                "NAA050.57.00.000 W001.21.24.490 ;comment2",
                "120.220 ;comment3"
            }}, // Invalid coordinates
        };

        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            this.RunParserOnLines(lines);
            Assert.Empty(this.sectorElementCollection.ActiveRunways);
            this.logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsAirportData()
        {
            this.RunParserOnLines(
                new List<string>(new string[] { "EGHI", "Southampton; comment1", "N050.57.00.000 W001.21.24.490 ;comment2", "120.220 ;comment3" })
            );

            Airport result = this.sectorElementCollection.Airports[0];
            Assert.Equal("Southampton", result.Name);
            Assert.Equal("EGHI", result.Icao);
            Assert.Equal("120.220", result.Frequency);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.LatLong);
            Assert.Equal("comment1, comment2, comment3", result.InlineComment.CommentString);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.SCT_AIRPORT_BASIC;
        }
    }
}
