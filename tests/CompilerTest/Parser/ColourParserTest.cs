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
    public class ColourParserTest
    {
        private readonly ColourParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public ColourParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (ColourParser)(new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.SCT_COLOUR_DEFS);
        }

        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "#define abc"
            }}, // Too few segments
            new object[] { new List<string>{
                "#nodefine abc 123"
            }}, // Not started with define
            new object[] { new List<string>{
                "#define abc 123.4"
            }}, // Value is a float
            new object[] { new List<string>{
                "#define abc 123abc"
            }}, // Value is a string
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

            Assert.Empty(this.collection.Colours);
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
            Assert.IsType<BlankLine>(
                this.collection.Compilables[OutputSections.SCT_COLOUR_DEFS][0]
            );
        }

        [Fact]
        public void TestItAddsColourData()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] {
                    "#define abc 255 ;comment"
                })
            );
            this.parser.ParseData(data);

            Colour result = this.collection.Colours[0];
            Assert.Equal("abc", result.Name);
            Assert.Equal(255, result.Value);
            Assert.Equal("comment", result.Comment);
        }
    }
}
