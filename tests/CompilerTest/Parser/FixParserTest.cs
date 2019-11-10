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
    public class FixParserTest
    {
        private readonly FixParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public FixParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = new FixParser(new MetadataParser(this.collection, OutputSections.SCT_FIXES), this.collection, this.log.Object);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorTooManySections()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "ABCDE N050.57.00.000 W001.21.24.490 MORE" })
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
                new List<string>(new string[] { "ABCDE N050.57.00.000W001.21.24.490" })
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
                new List<string>(new string[] { "ABCDE N050.57.00.000 N001.21.24.490" })
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
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.SCT_FIXES][0]);
        }

        [Fact]
        public void TestItAddsFixData()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "ABCDE N050.57.00.000 W001.21.24.490;comment" })
            );
            this.parser.ParseData(data);

            Fix result = this.collection.Fixes[0];
            Assert.Equal("ABCDE", result.Identifier);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Coordinate);
            Assert.Equal(" ;comment", result.Comment);
        }
    }
}
