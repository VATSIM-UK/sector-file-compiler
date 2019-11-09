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
    public class SidStarParserTest
    {
        private readonly SidStarParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public SidStarParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = new SidStarParser(new MetadataParser(this.collection, OutputSections.ESE_SIDSSTARS), this.collection, this.log.Object);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfIncorrectNumberOfSegments()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                new List<string>(new string[] { "abc:def:ghi" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesAnErrorIfUnknownType()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                new List<string>(new string[] { "abc:def:ghi:jkl:mno" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItHandlesMetadata()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                new List<string>(new string[] { "" })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.ESE_SIDSSTARS][0]);
        }

        [Fact]
        public void TestItAddsSidStarData()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                new List<string>(new string[] { "SID:EGKK:26L:ADMAG2X:FIX1 FIX2 ;comment" })
            );

            this.parser.ParseData(data);

            SidStar result = this.collection.SidStars[0];
            Assert.Equal("SID", result.Type);
            Assert.Equal("EGKK", result.Airport);
            Assert.Equal("26L", result.Runway);
            Assert.Equal("ADMAG2X", result.Identifier);
            Assert.Equal(new List<string>(new string[] { "FIX1", "FIX2" }), result.Route);
            Assert.Equal(" ;comment", result.Comment);
        }
    }
}
