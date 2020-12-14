using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Parser;
using Compiler.Error;
using Compiler.Model;
using Compiler.Event;
using CompilerTest.Mock;
using Compiler.Output;

namespace CompilerTest.Parser
{
    public class AirportParserTest
    {
        private readonly AirportParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public AirportParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (AirportParser) (new DataParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSectionKeys.SCT_AIRPORT);
        }

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
            this.parser.ParseData(
                new MockSectorDataFile(
                    "test.txt",
                    lines
                )
            );

            Assert.Empty(this.collection.ActiveRunways);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsAirportData()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "EGHI", "Southampton; comment1", "N050.57.00.000 W001.21.24.490 ;comment2", "120.220 ;comment3" })
            );

            this.parser.ParseData(data);

            Airport result = this.collection.Airports[0];
            Assert.Equal("Southampton", result.Name);
            Assert.Equal("EGHI", result.Icao);
            Assert.Equal("120.220", result.Frequency);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.LatLong);
            Assert.Equal("comment1, comment2, comment3", result.Comment);
        }

        [Fact]
        public void TestItAddsAirportDataNoLineComments()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "EGHI", "Southampton", "N050.57.00.000 W001.21.24.490", "120.220" })
            );

            this.parser.ParseData(data);

            Airport result = this.collection.Airports[0];
            Assert.Equal("Southampton", result.Name);
            Assert.Equal("EGHI", result.Icao);
            Assert.Equal("120.220", result.Frequency);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.LatLong);
            Assert.Equal("", result.Comment);
        }

        [Fact]
        public void TestItAddsAirportDataWithMetadataBetweenLines()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { ";comment1", "EGHI", "Southampton; comment1", ";comment2", "N050.57.00.000 W001.21.24.490 ;comment2", ";comment3", "120.220 ;comment3", ";comment4" })
            );

            this.parser.ParseData(data);

            Airport result = this.collection.Airports[0];
            Assert.Equal("Southampton", result.Name);
            Assert.Equal("EGHI", result.Icao);
            Assert.Equal("120.220", result.Frequency);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.LatLong);
            Assert.Equal("comment1, comment2, comment3", result.Comment);
        }
    }
}
