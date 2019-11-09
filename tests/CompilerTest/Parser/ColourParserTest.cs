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
    public class ColourParserTest
    {
        private readonly ColourParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public ColourParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = new ColourParser(new MetadataParser(this.collection, OutputSections.SCT_COLOUR_DEFS), this.collection, this.log.Object);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfIncorrectNumberOfSegments()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                new List<string>(new string[] { "#define abc" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfNotStartedByDefine()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                new List<string>(new string[] { "#nodefine abc 123" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfColourIsFloat()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                new List<string>(new string[] { "#define abc 123.4" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfColourIsString()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                new List<string>(new string[] { "#define abc 123abc" })
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
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.SCT_COLOUR_DEFS][0]);
        }

        [Fact]
        public void TestItAddsColourData()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                new List<string>(new string[] { "#define abc 255 ;comment" })
            );
            this.parser.ParseData(data);

            Colour result = this.collection.Colours[0];
            Assert.Equal("abc", result.Name);
            Assert.Equal(255, result.Value);
            Assert.Equal(" ;comment", result.Comment);
        }
    }
}
