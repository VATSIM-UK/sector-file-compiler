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
    public class EsePositionParserTest
    {
        private readonly EsePositionParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public EsePositionParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (EsePositionParser)(new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.ESE_POSITIONS);
        }

        [Fact]
        public void TestItAddsPositionData()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:W002.43.29.830:N051.32.26.871:W002.43.29.831:N051.32.26.872:W002.43.29.832:N051.32.26.873:W002.43.29.833 ;comment" })
            );

            this.parser.ParseData(data);

            List<Coordinate> coordinateList = new List<Coordinate>();
            coordinateList.Add(new Coordinate("N051.32.26.870", "W002.43.29.830"));
            coordinateList.Add(new Coordinate("N051.32.26.871", "W002.43.29.831"));
            coordinateList.Add(new Coordinate("N051.32.26.872", "W002.43.29.832"));
            coordinateList.Add(new Coordinate("N051.32.26.873", "W002.43.29.833"));
            ControllerPosition position = this.collection.EsePositions[0];
            Assert.Equal("LON_CTR", position.Callsign);
            Assert.Equal("London Control", position.RtfCallsign);
            Assert.Equal("127.820", position.Frequency);
            Assert.Equal("L", position.Identifier);
            Assert.Equal("9", position.MiddleLetter);
            Assert.Equal("LON", position.Prefix);
            Assert.Equal("CTR", position.Suffix);
            Assert.Equal("0301", position.SquawkRangeStart);
            Assert.Equal("0377", position.SquawkRangeEnd);
            Assert.Equal(coordinateList, position.VisCentres);
        }

        [Fact]
        public void TestItAddsPositionDataSkippedCenters()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:W002.43.29.830:::::: ;comment" })
            );

            this.parser.ParseData(data);

            List<Coordinate> coordinateList = new List<Coordinate>();
            coordinateList.Add(new Coordinate("N051.32.26.870", "W002.43.29.830"));
            ControllerPosition position = this.collection.EsePositions[0];
            Assert.Equal("LON_CTR", position.Callsign);
            Assert.Equal("London Control", position.RtfCallsign);
            Assert.Equal("127.820", position.Frequency);
            Assert.Equal("L", position.Identifier);
            Assert.Equal("9", position.MiddleLetter);
            Assert.Equal("LON", position.Prefix);
            Assert.Equal("CTR", position.Suffix);
            Assert.Equal("0301", position.SquawkRangeStart);
            Assert.Equal("0377", position.SquawkRangeEnd);
            Assert.Equal(coordinateList, position.VisCentres);
        }

        [Fact]
        public void TestItAddsPositionDataSkippedSquawks()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-::-:N051.32.26.870:W002.43.29.830 ;comment" })
            );

            this.parser.ParseData(data);

            List<Coordinate> coordinateList = new List<Coordinate>();
            coordinateList.Add(new Coordinate("N051.32.26.870", "W002.43.29.830"));
            ControllerPosition position = this.collection.EsePositions[0];
            Assert.Equal("LON_CTR", position.Callsign);
            Assert.Equal("London Control", position.RtfCallsign);
            Assert.Equal("127.820", position.Frequency);
            Assert.Equal("L", position.Identifier);
            Assert.Equal("9", position.MiddleLetter);
            Assert.Equal("LON", position.Prefix);
            Assert.Equal("CTR", position.Suffix);
            Assert.Equal("", position.SquawkRangeStart);
            Assert.Equal("-", position.SquawkRangeEnd);
            Assert.Equal(coordinateList, position.VisCentres);
        }

        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301 ;comment"
            }}, // Incorrect number of segments
            new object[] { new List<string>{
                "LON_CTR:London Control:127.821:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:W002.43.29.830 ;comment"
            }}, // Invalid RTF frequency
            new object[] { new List<string>{
                "LON_CTR:London Control:127.820:L:9:LON:LOL:-:-:0301:0377:N051.32.26.870:W002.43.29.830 ;comment"
            }}, // Invalid suffix
            new object[] { new List<string>{
                "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0381:0377:N051.32.26.870:W002.43.29.830 ;comment"
            }}, // Invalid squawk range start
            new object[] { new List<string>{
                "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0371xxxx:0377:N051.32.26.870:W002.43.29.830 ;comment"
            }}, // Squawk range start has extra characters
            new object[] { new List<string>{
                "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0378:N051.32.26.870:W002.43.29.830 ;comment"
            }}, // Invalid squawk range end
            new object[] { new List<string>{
                "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377xxxxx:N051.32.26.870:W002.43.29.830 ;comment"
            }}, // Squawk range end has extra characters
            new object[] { new List<string>{
                "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:W002.43.29.830:::::::: ;comment"
            }}, // Too many vis centers
            new object[] { new List<string>{
                "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:W002.43.29.830: ;comment"
            }}, // Vis center missing longitude
            new object[] { new List<string>{
                "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:N002.43.29.830 ;comment"
            }}, // Invalid vis center coordinate
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

            Assert.Empty(this.collection.EsePositions);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }
    }
}
