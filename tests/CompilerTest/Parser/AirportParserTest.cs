using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class AirportParserTest: AbstractParserTestCase
    {
        public AirportParserTest()
        {
            SetInputFileName("EGHI/Basic.txt");
        }
        
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "Southampton; comment1",
                "N050.57.00.000 W001.21.24.490 ;comment2",
                "120.220 ;comment3",
                "Another line!"
            }}, // Too many lines
            new object[] { new List<string>{
                "Southampton; comment1",
                "N050.57.00.000 W001.21.24.490 ;comment2"
            }}, // Too few lines
            new object[] { new List<string>{
                "Southampton; comment1",
                "N050.57.00.000W001.21.24.490 ;comment2",
                "120.220 ;comment3"
            }}, // Invalid coordinate format
            new object[] { new List<string>{
                "Southampton; comment1",
                "NAA050.57.00.000 W001.21.24.490 ;comment2",
                "120.220 ;comment3"
            }}, // Invalid coordinates
        };

        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            RunParserOnLines(lines);
            Assert.Empty(sectorElementCollection.ActiveRunways);
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }
        
        [Fact]
        public void ItRaisesSyntaxErrorOnBadFolderName()
        {
            SetInputFileName("1234/Basic.txt");
            RunParserOnLines(new List<string>{
                "Southampton; comment1",
                "N050.57.00.000 W001.21.24.490 ;comment2",
                "120.220 ;comment3"
            });
            Assert.Empty(sectorElementCollection.ActiveRunways);
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsAirportData()
        {
            RunParserOnLines(
                new List<string>(new[] {"Southampton; comment1", "N050.57.00.000 W001.21.24.490 ;comment2", "120.220 ;comment3" })
            );

            Airport result = sectorElementCollection.Airports[0];
            Assert.Equal("Southampton", result.Name);
            Assert.Equal("EGHI", result.Icao);
            Assert.Equal("120.220", result.Frequency);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.LatLong);
            AssertExpectedMetadata(result, commentString: "comment1");
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.SCT_AIRPORT_BASIC;
        }
    }
}
