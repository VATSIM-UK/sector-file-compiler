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
    public class ActiveRunwayParserTest
    {
        private readonly ActiveRunwayParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public ActiveRunwayParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (ActiveRunwayParser) (new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.RWY_ACTIVE_RUNWAYS);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorTooManySegments()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "ACTIVE_RUNWAY:EGHI:20:1:EXTRA ;comment" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorTooFewSegments()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "ACTIVE_RUNWAY:EGHI:20 ;comment" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorIncorrectDeclaration()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "ACTIVE_RUNWAYS:EGHI:20:1 ;comment" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorInalidIcao()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "ACTIVE_RUNWAY:ASD1:20:1 ;comment" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorInvalidRunway()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "ACTIVE_RUNWAY:EGHI:37:1 ;comment" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxInvalidMode()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "ACTIVE_RUNWAY:EGHI:20:2 ;comment" })
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
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.RWY_ACTIVE_RUNWAYS][0]);
        }

        [Fact]
        public void TestItAddsDataInMode0()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
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
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
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
