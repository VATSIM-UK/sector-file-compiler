using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Parser;
using Compiler.Error;
using Compiler.Model;
using Compiler.Event;
using Compiler.Output;
using CompilerTest.Mock;

namespace CompilerTest.Parser
{
    public class AirwayParserTest
    {
        private readonly AirwayParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public AirwayParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (AirwayParser)(new DataParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.SCT_LOW_AIRWAY);
        }

        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "BHD BHD DIKAS DIKAS"
            }}, // Too few segments
            new object[] { new List<string>{
                "N864 BHD BHD DIKAS DIKAS EXMOR"
            }}, // Too many segments
            new object[] { new List<string>{
                "N864 N050.57.00.000 W001.21.24.490 N050.57.00.000 N001.21.24.490"
            }}, // Invalid end point
            new object[] { new List<string>{
                "N864 N050.57.00.000 N001.21.24.490 N050.57.00.000 W001.21.24.490"
            }}, // Invalid start point
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

            Assert.Empty(this.collection.HighAirways);
            Assert.Empty(this.collection.LowAirways);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItHandlesMetadata()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "UN864.txt",
                new List<string>(new string[] { "" })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(
                this.collection.Compilables[OutputSections.SCT_LOW_AIRWAY][0]
            );
        }

        [Fact]
        public void TestItAddsAirwayData()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "UN864.txt",
                new List<string>(new string[] { "UN864 N050.57.00.001 W001.21.24.490 N050.57.00.002 W001.21.24.490;comment" })
            );
            this.parser.ParseData(data);

            Airway result = this.collection.LowAirways[0];
            Assert.Equal("UN864", result.Identifier);
            Assert.Equal(AirwayType.LOW, result.Type);
            Assert.Equal(new Point(new Coordinate("N050.57.00.001", "W001.21.24.490")), result.StartPoint);
            Assert.Equal(new Point(new Coordinate("N050.57.00.002", "W001.21.24.490")), result.EndPoint);
            Assert.Equal("comment", result.Comment);
        }

        [Fact]
        public void TestItAddsAirwayDataWithIdentifiers()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "UN864.txt",
                new List<string>(new string[] { "UN864 DIKAS DIKAS BHD BHD;comment" })
            );
            this.parser.ParseData(data);

            Airway result = this.collection.LowAirways[0];
            Assert.Equal("UN864", result.Identifier);
            Assert.Equal(AirwayType.LOW, result.Type);
            Assert.Equal(new Point("DIKAS"), result.StartPoint);
            Assert.Equal(new Point("BHD"), result.EndPoint);
            Assert.Equal("comment", result.Comment);
        }
    }
}
