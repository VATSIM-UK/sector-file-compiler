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
    public class RegionParserTest
    {
        private readonly RegionParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public RegionParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (RegionParser)(new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.SCT_REGIONS);
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
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.SCT_REGIONS][0]);
        }

        [Fact]
        public void TestItAddsSingleLineRegionData()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "Red BCN BCN ;comment" })
            );
            this.parser.ParseData(data);

            Region result = this.collection.Regions[0];
            Assert.Single(result.Points);
            Assert.Equal(new Point("BCN"), result.Points[0]);
            Assert.Equal("Red", result.Colour);
            Assert.Equal("comment", result.Comment);
        }

        [Fact]
        public void TestItAddsMultipleLineRegionData()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "Red BCN BCN ;comment", " BHD BHD", " JSY JSY" })
            );
            this.parser.ParseData(data);

            Region result = this.collection.Regions[0];
            Assert.Equal(3, result.Points.Count);
            Assert.Equal(new Point("BCN"), result.Points[0]);
            Assert.Equal(new Point("BHD"), result.Points[1]);
            Assert.Equal(new Point("JSY"), result.Points[2]);
            Assert.Equal("Red", result.Colour);
            Assert.Equal("comment", result.Comment);
        }

        [Fact]
        public void TestItAddsMultipleRegionsData()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "Red BCN BCN ;comment", " BHD BHD", "White JSY JSY" })
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

            Assert.Single(result2.Points);
            Assert.Equal(new Point("JSY"), result2.Points[0]);
            Assert.Equal("White", result2.Colour);
            Assert.Null(result2.Comment);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorInvalidFirstLine()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "BCN BCN ;comment", " BHD BHD"})
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
            Assert.Empty(this.collection.Regions);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorInvalidFirstLinePoint()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "Red BCN BHD ;comment", " BHD BHD"})
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
            Assert.Empty(this.collection.Regions);
        }

        [Fact]
        public void TestItRaisesSyntaErrorNonNewSegmentsDonStartWithSpace()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "Red BCN BCN ;comment", "BHD BHD"})
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
            Assert.Empty(this.collection.Regions);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorContinuationPointInvalid()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "Red BCN BCN ;comment", "BHD MID" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
            Assert.Empty(this.collection.Regions);
        }
    }
}
