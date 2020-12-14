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
    public class FixParserTest
    {
        private readonly FixParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public FixParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (FixParser)(new DataParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSectionKeys.SCT_FIXES);
        }

        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "ABCDE N050.57.00.000 W001.21.24.490 MORE"
            }}, // Too many sections
            new object[] { new List<string>{
                "ABCDE N050.57.00.000W001.21.24.490"
            }}, // Too few sections
            new object[] { new List<string>{
                "ABCDE N050.57.00.000 N001.21.24.490"
            }}, // Invalid coordinates
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

            Assert.Empty(this.collection.Fixes);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItHandlesMetadata()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "" })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSectionKeys.SCT_FIXES][0]);
        }

        [Fact]
        public void TestItAddsFixData()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "ABCDE N050.57.00.000 W001.21.24.490;comment" })
            );
            this.parser.ParseData(data);

            Fix result = this.collection.Fixes[0];
            Assert.Equal("ABCDE", result.Identifier);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Coordinate);
            Assert.Equal("comment", result.Comment);
        }
    }
}
