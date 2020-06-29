using Moq;
using Compiler.Parser;
using Compiler.Model;
using Compiler.Event;
using Compiler.Output;
using Xunit;
using Compiler.Error;
using System.Collections.Generic;

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
            this.parser = (AirspaceParser)(new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.ESE_AIRSPACE);
        }

        [Fact]
        public void ItRaisesSyntaxErrorInvalidDeclaration()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
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
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "" })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.ESE_AIRSPACE][0]);
        }
    }
}
