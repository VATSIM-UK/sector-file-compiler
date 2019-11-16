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
    public class VorParserTest
    {
        private readonly VorParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public VorParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (VorParser)(new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.SCT_VOR);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorTooManySections()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "BHD 112.050 N050.57.00.000 W001.21.24.490 MORE" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorTooFewSections()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "BHD 112.050 N050.57.00.000" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorIdentifierContainsNumbers()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "BH1 112.050 N050.57.00.000 W001.21.24.490" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorIdentifierTooLong()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "BHDD 112.050 N050.57.00.000 W001.21.24.490" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorInvalidFrequency()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "BHD abc.050 N050.57.00.000 W001.21.24.490" })
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
                new List<string>(new string[] { "BHD 112.050 NA50.57.00.000 W001.21.24.490" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItHandlesMetadata()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "test",
                new List<string>(new string[] { "" })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.SCT_VOR][0]);
        }

        [Fact]
        public void TestItAddsVorData()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "BHD 112.050 N050.57.00.000 W001.21.24.490;comment" })
            );
            this.parser.ParseData(data);

            Vor result = this.collection.Vors[0];
            Assert.Equal("BHD", result.Identifier);
            Assert.Equal("112.050", result.Frequency);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Coordinate);
            Assert.Equal("comment", result.Comment);
        }
    }
}
