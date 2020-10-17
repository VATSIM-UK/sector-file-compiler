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
    public class VorParserTest
    {
        private readonly VorParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public VorParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (VorParser)(new DataParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.SCT_VOR);
        }

        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "BHD 112.050 N050.57.00.000 W001.21.24.490 MORE"
            }}, // Too many sections
            new object[] { new List<string>{
                "BHD 112.050 N050.57.00.000"
            }}, // Too few sections
            new object[] { new List<string>{
                "BH1 112.050 N050.57.00.000 W001.21.24.490"
            }}, // Invalid identifier - contains numbers
            new object[] { new List<string>{
                "BHDD 112.050 N050.57.00.000 W001.21.24.490"
            }}, // Invalid identifier - too long
            new object[] { new List<string>{
                "B 112.050 N050.57.00.000 W001.21.24.490"
            }}, // Invalid identifier - too short
            new object[] { new List<string>{
                "BHD abc.500 N050.57.00.000 W001.21.24.490"
            }}, // Invalid frequency
            new object[] { new List<string>{
                "BHD 112.050 NA50.57.00.000 W001.21.24.490"
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

            Assert.Empty(this.collection.Vors);
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
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.SCT_VOR][0]);
        }

        [Fact]
        public void TestItAddsVorData()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "BHD 112.050 N050.57.00.000 W001.21.24.490;comment" })
            );
            this.parser.ParseData(data);

            Vor result = this.collection.Vors[0];
            Assert.Equal("BHD", result.Identifier);
            Assert.Equal("112.050", result.Frequency);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Coordinate);
            Assert.Equal("comment", result.Comment);
        }

        [Fact]
        public void TestItAddsVorDataTwoLetterIdentifier()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "BH 112.050 N050.57.00.000 W001.21.24.490;comment" })
            );
            this.parser.ParseData(data);

            Vor result = this.collection.Vors[0];
            Assert.Equal("BH", result.Identifier);
            Assert.Equal("112.050", result.Frequency);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Coordinate);
            Assert.Equal("comment", result.Comment);
        }
    }
}
