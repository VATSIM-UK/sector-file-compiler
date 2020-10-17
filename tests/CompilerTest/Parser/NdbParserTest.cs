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
    public class NdbParserTest
    {
        private readonly NdbParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public NdbParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (NdbParser)(new DataParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.SCT_NDB);
        }

        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "CDF 388.500 N050.57.00.000 W001.21.24.490 MORE"
            }}, // Too many sections
            new object[] { new List<string>{
                "CDF 388.500 N050.57.00.000"
            }}, // Too few sections
            new object[] { new List<string>{
                "BH1 388.500 N050.57.00.000 W001.21.24.490"
            }}, // Invalid identifier - contains numbers
            new object[] { new List<string>{
                "CDFF 388.500 N050.57.00.000 W001.21.24.490"
            }}, // Invalid identifier - too long
            new object[] { new List<string>{
                "CFG abc.500 N050.57.00.000 W001.21.24.490"
            }}, // Invalid frequency
            new object[] { new List<string>{
                "CDF 388.500 NA50.57.00.000 W001.21.24.490"
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

            Assert.Empty(this.collection.Ndbs);
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
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.SCT_NDB][0]);
        }

        [Fact]
        public void TestItAddsNdbData()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
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
