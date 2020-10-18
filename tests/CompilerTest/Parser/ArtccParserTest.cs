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
    public class ArtccParserTest
    {
        private readonly ArtccParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public ArtccParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (ArtccParser)(new DataParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.SCT_ARTCC);
        }

        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "EGTT London FIR"
            }}, // Too few segments
            new object[] { new List<string>{
                "EGTT London FIR N050.57.00.000 W001.21.24.490 N050.57.00.000 N001.21.24.490"
            }}, // Invalid end point
            new object[] { new List<string>{
                "EGTT London FIR N050.57.00.000 N001.21.24.490 N050.57.00.000 W001.21.24.490"
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

            Assert.Empty(this.collection.Artccs);
            Assert.Empty(this.collection.HighArtccs);
            Assert.Empty(this.collection.LowArtccs);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItHandlesMetadata()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "" })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.SCT_ARTCC][0]);
        }

        [Fact]
        public void TestItAddsAirwayData()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "EGTT London FIR   N050.57.00.001 W001.21.24.490 N050.57.00.002 W001.21.24.490;comment" })
            );
            this.parser.ParseData(data);

            ArtccSegment result = this.collection.Artccs[0];
            Assert.Equal("EGTT London FIR", result.Identifier);
            Assert.Equal(ArtccType.REGULAR, result.Type);
            Assert.Equal(new Point(new Coordinate("N050.57.00.001", "W001.21.24.490")), result.StartPoint);
            Assert.Equal(new Point(new Coordinate("N050.57.00.002", "W001.21.24.490")), result.EndPoint);
            Assert.Equal("comment", result.Comment);
        }

        [Fact]
        public void TestItAddsAirwayDataWithIdentifiers()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "EGTT London FIR   DIKAS DIKAS BHD BHD;comment" })
            );
            this.parser.ParseData(data);

            ArtccSegment result = this.collection.Artccs[0];
            Assert.Equal("EGTT London FIR", result.Identifier);
            Assert.Equal(ArtccType.REGULAR, result.Type);
            Assert.Equal(new Point("DIKAS"), result.StartPoint);
            Assert.Equal(new Point("BHD"), result.EndPoint);
            Assert.Equal("comment", result.Comment);
        }
    }
}
