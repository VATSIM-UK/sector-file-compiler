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
    public class RegionParserTest
    {
        private readonly RegionParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public RegionParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (RegionParser)(new DataParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSectionKeys.SCT_REGIONS);
        }

        [Fact]
        public void TestItHandlesMetadata()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "" })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSectionKeys.SCT_REGIONS][0]);
        }

        [Fact]
        public void TestItAddsSinglePointRegionData()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "REGIONNAME TestRegion", "Red BCN BCN ;comment" })
            );
            this.parser.ParseData(data);

            Region result = this.collection.Regions[0];
            Assert.Single(result.Points);
            Assert.Equal(new Point("BCN"), result.Points[0]);
            Assert.Equal("Red", result.Colour);
            Assert.Equal("comment", result.Comment);
            Assert.Equal("TestRegion", result.Name);
        }

        [Fact]
        public void TestItAddsMultipleLineRegionData()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "REGIONNAME TestRegion", "Red BCN BCN ;comment", "BHD BHD", " JSY JSY" })
            );
            this.parser.ParseData(data);

            Region result = this.collection.Regions[0];
            Assert.Equal(3, result.Points.Count);
            Assert.Equal(new Point("BCN"), result.Points[0]);
            Assert.Equal(new Point("BHD"), result.Points[1]);
            Assert.Equal(new Point("JSY"), result.Points[2]);
            Assert.Equal("Red", result.Colour);
            Assert.Equal("comment", result.Comment);
            Assert.Equal("TestRegion", result.Name);
        }

        [Fact]
        public void TestItAddsMultipleRegionsData()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { 
                    "REGIONNAME TestRegion1",
                    "Red BCN BCN ;comment",
                     " BHD BHD",
                    "REGIONNAME TestRegion2",
                    "White JSY JSY"
                })
            );
            this.parser.ParseData(data);

            Assert.Equal(2, this.collection.Regions.Count);
            Region result1 = this.collection.Regions[0];
            Region result2 = this.collection.Regions[1];
            

            Assert.Equal(2, result1.Points.Count);
            Assert.Equal(new Point("BCN"), result1.Points[0]);
            Assert.Equal(new Point("BHD"), result1.Points[1]);
            Assert.Equal("Red", result1.Colour);
            Assert.Equal("comment", result1.Comment);
            Assert.Equal("TestRegion1", result1.Name);

            Assert.Single(result2.Points);
            Assert.Equal(new Point("JSY"), result2.Points[0]);
            Assert.Equal("White", result2.Colour);
            Assert.Null(result2.Comment);
            Assert.Equal("TestRegion2", result2.Name);
        }

        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "BCN BCN ;comment", " BHD BHD"
            }}, // Invalid first line
            new object[] { new List<string>{
                "REGIONNAME TestRegion", "Red BCN BHD ;comment",
                " BHD BHD"
            }}, // Invalid first region point
            new object[] { new List<string>{
                "REGIONNAME TestRegion", "Red BCN BCN ;comment",
                "BHD MID"
            }}, // Invalid continuation point
            new object[] { new List<string>{
                "REGIONNAME TestRegion",
                "Red BCN BCN ;comment",
                "Red BCN BCN ;comment"
            }}, // Unexpected colour
            new object[] { new List<string>{
                "REGIONNAME TestRegion",
                "BCN BCN ;comment"
            }}, // No colour
            new object[] { new List<string>{
                "REGIONNAME TestRegion2"
            }}, // Incomplete region at end of file
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

            Assert.Empty(this.collection.Regions);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }
    }
}
