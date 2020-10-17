using Moq;
using Compiler.Parser;
using Compiler.Model;
using Compiler.Event;
using Compiler.Output;
using Xunit;
using Compiler.Error;
using System.Collections.Generic;
using CompilerTest.Mock;

namespace CompilerTest.Parser
{
    public class AirspaceParserTest
    {
        private readonly AirspaceParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public AirspaceParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (AirspaceParser)(new DataParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.ESE_AIRSPACE);
        }

        [Fact]
        public void ItRaisesSyntaxErrorInvalidDeclaration()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] {
                    "NOPE_SECTORLINE:BBTWR:EGBB:2.5 ;comment",
                    "DISPLAY:BBAPP:BBAPP:BBTWR ;comment1",
                    "DISPLAY:BBTWR:BBAPP:BBTWR ;comment2",
                })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItHandlesMetadata()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] {
                    ""
                })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.ESE_AIRSPACE][0]);
        }

        [Fact]
        public void TestItDealsWithData()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] {
                    "FIR_COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:*:25000:|HEMEL20 ;comment",
                    "FIR_COPX:*:*:HEMEL:EGNX:*:London AC Worthing:London AC Dover:*:25000:|HEMEL20 ;comment"
                })
            );

            this.parser.ParseData(data);
            Assert.Equal(2, this.collection.CoordinationPoints.Count);
            Assert.IsType<CoordinationPoint>(this.collection.Compilables[OutputSections.ESE_AIRSPACE][0]);
            Assert.IsType<CoordinationPoint>(this.collection.Compilables[OutputSections.ESE_AIRSPACE][1]);
        }
    }
}
