using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Parser;
using Compiler.Error;
using Compiler.Model;
using Compiler.Event;
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
            this.parser = (AirportParser) (new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.SCT_AIRPORT);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorTooManyLines()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "Southampton; comment1", "N050.57.00.000 W001.21.24.490 ;comment2", "120.220 ;comment3",  "Another line!" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorTooFewLines()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "Southampton; comment1", "N050.57.00.000 W001.21.24.490 ;comment2" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorInvalidCoordinateFormat()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "Southampton; comment1", "N050.57.00.000W001.21.24.490 ;comment2", "120.220 ;comment3" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorInvalidCoordinates()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "Southampton; comment1", "NAA050.57.00.000 W001.21.24.490 ;comment2", "120.220 ;comment3" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsAirportData()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "Southampton; comment1", "N050.57.00.000 W001.21.24.490 ;comment2", "120.220 ;comment3" })
            );
            this.parser.ParseData(data);

            Airport result = this.collection.Airports[0];
            Assert.Equal("Southampton", result.Name);
            Assert.Equal("EGHI", result.Icao);
            Assert.Equal("120.220", result.Frequency);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.LatLong);
            Assert.Equal(" ;comment1, comment2, comment3", result.Comment);
        }

        [Fact]
        public void TestItAddsAirportDataNoLineComments()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "Southampton", "N050.57.00.000 W001.21.24.490", "120.220" })
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
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { ";comment1", "Southampton; comment1", ";comment2", "N050.57.00.000 W001.21.24.490 ;comment2", ";comment3", "120.220 ;comment3", ";comment4" })
            );
            this.parser.ParseData(data);

            Airport result = this.collection.Airports[0];
            Assert.Equal("Southampton", result.Name);
            Assert.Equal("EGHI", result.Icao);
            Assert.Equal("120.220", result.Frequency);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.LatLong);
            Assert.Equal(" ;comment1, comment2, comment3", result.Comment);
        }
    }
}
