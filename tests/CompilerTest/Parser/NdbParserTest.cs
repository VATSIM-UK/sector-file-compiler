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
    public class NdbParserTest
    {
        private readonly NdbParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public NdbParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (NdbParser)(new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.SCT_NDB);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorTooManySections()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "CDF 388.500 N050.57.00.000 W001.21.24.490 MORE" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorTooFewSections()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "CDF 388.500 N050.57.00.000" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorIdentifierContainsNumbers()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "BH1 388.500 N050.57.00.000 W001.21.24.490" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorIdentifierTooLong()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "CDFF 388.500 N050.57.00.000 W001.21.24.490" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorInvalidFrequency()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "CFG abc.500 N050.57.00.000 W001.21.24.490" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorInvalidCoordinates()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "CDF 388.500 NA50.57.00.000 W001.21.24.490" })
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
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.SCT_NDB][Subsections.DEFAULT][0]);
        }

        [Fact]
        public void TestItAddsNdbData()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "CDF 388.500 N050.57.00.000 W001.21.24.490;comment" })
            );
            this.parser.ParseData(data);

            Ndb result = this.collection.Ndbs[0];
            Assert.Equal("CDF", result.Identifier);
            Assert.Equal("388.500", result.Frequency);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Coordinate);
            Assert.Equal("comment", result.Comment);
        }
    }
}
