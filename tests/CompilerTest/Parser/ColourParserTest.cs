using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Parser;
using Compiler.Error;
using Compiler.Model;
using Compiler.Event;

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
            this.parser = new ColourParser(this.log.Object);
            this.collection = new SectorElementCollection();
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfIncorrectNumberOfSegments()
        {
            this.parser.ParseElements("test.txt", new List<string>(new string[] { "#define abc" }), this.collection);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfNotStartedByDefine()
        {
            this.parser.ParseElements("test.txt", new List<string>(new string[] { "#nodefine abc 123" }), this.collection);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfColourIsFloat()
        {
            this.parser.ParseElements("test.txt", new List<string>(new string[] { "#define abc 123.4" }), this.collection);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfColourIsString()
        {
            this.parser.ParseElements("test.txt", new List<string>(new string[] { "#define abc 123abc" }), this.collection);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsColourData()
        {
            this.parser.ParseElements(
                "test.txt",
                new List<string>(new string[] { "#define abc 255" }),
                this.collection
            );

            Colour result = this.collection.Colours[0];
            Assert.Equal("abc", result.Name);
            Assert.Equal(255, result.Value);
        }
    }
}
