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
    public class ActiveRunwayParserTest
    {
        private readonly ActiveRunwayParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public ActiveRunwayParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (ActiveRunwayParser) (new DataParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.RWY_ACTIVE_RUNWAYS);
        }

        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{ "ACTIVE_RUNWAY:EGHI:20:1:EXTRA ;comment" }}, // Too many segments
            new object[] { new List<string>{ "ACTIVE_RUNWAY:EGHI:20 ;comment" }}, // Too few segments
            new object[] { new List<string>{ "ACTIVE_RUNWAYS:EGHI:20:1 ;comment" } }, // Bad declaration
            new object[] { new List<string>{ "ACTIVE_RUNWAY:ASD1:20:1 ;comment" } }, // Invalid icao
            new object[] { new List<string>{ "ACTIVE_RUNWAY:EGHI:37:1 ;comment" } }, // Invalid runway
            new object[] { new List<string>{ "ACTIVE_RUNWAY:EGHI:20:2 ;comment" } }, // Invalid mode
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
        public void TestItHandlesMetadata()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "" })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.RWY_ACTIVE_RUNWAYS][0]);
        }

        [Fact]
        public void TestItAddsDataInMode0()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "ACTIVE_RUNWAY:EGHI:20:0 ;comment" })
            );

            this.parser.ParseData(data);

            ActiveRunway result = this.collection.ActiveRunways[0];
            Assert.Equal("EGHI", result.Airfield);
            Assert.Equal("20", result.Identifier);
            Assert.Equal(0, result.Mode);
            Assert.Equal("comment", result.Comment);
        }

        [Fact]
        public void TestItAddsDataInMode1()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "ACTIVE_RUNWAY:EGHI:20:1 ;comment" })
            );

            this.parser.ParseData(data);

            ActiveRunway result = this.collection.ActiveRunways[0];
            Assert.Equal("EGHI", result.Airfield);
            Assert.Equal("20", result.Identifier);
            Assert.Equal(1, result.Mode);
            Assert.Equal("comment", result.Comment);
        }
    }
}
