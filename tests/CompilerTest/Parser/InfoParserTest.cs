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
    public class InfoParserTest
    {
        private readonly InfoParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public InfoParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (InfoParser)(new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.SCT_INFO);
        }

        [Fact]
        public void TestItAddsInfoData()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] {
                    "UK (EGTT and EGPX) {VERSION}",
                    "LON_CTR",
                    "EGLL",
                    "N053.03.32.931",
                    "W001.00.00.000",
                    "60",
                    "36.06",
                    "-1.0",
                    "10",
                })
            );
            this.parser.ParseData(data);

            Info result = this.collection.Info;
            Assert.Equal("UK (EGTT and EGPX) {VERSION}", result.Name);
            Assert.Equal("LON_CTR", result.Callsign);
            Assert.Equal("EGLL", result.Airport);
            Assert.Equal(new Coordinate("N053.03.32.931", "W001.00.00.000"), result.Coordinate);
            Assert.Equal(60, result.MilesPerDegreeLatitude);
            Assert.Equal(36.06, result.MilesPerDegreeLongitude);
            Assert.Equal(-1.0, result.MagneticVariation);
            Assert.Equal(10, result.Scale);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorForNotEnoughData()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] {
                    "UK (EGTT and EGPX) {VERSION}",
                    "LON_CTR",
                    "EGLL",
                    "N053.03.32.931",
                    "W001.00.00.000",
                    "60",
                    "36.06",
                    "-1.0",
                })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorForInvalidLatitude()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] {
                    "UK (EGTT and EGPX) {VERSION}",
                    "LON_CTR",
                    "EGLL",
                    "abc",
                    "W001.00.00.000",
                    "60",
                    "36.06",
                    "-1.0",
                    "10",
                })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorForInvalidLongitude()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] {
                    "UK (EGTT and EGPX) {VERSION}",
                    "LON_CTR",
                    "EGLL",
                    "N053.03.32.931",
                    "abc",
                    "60",
                    "36.06",
                    "-1.0",
                    "10",
                })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorForInvalidMilesPerLatitude()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] {
                    "UK (EGTT and EGPX) {VERSION}",
                    "LON_CTR",
                    "EGLL",
                    "N053.03.32.931",
                    "W001.00.00.000",
                    "abc",
                    "36.06",
                    "-1.0",
                    "10",
                })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorForInvalidMilesPerLongitude()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] {
                    "UK (EGTT and EGPX) {VERSION}",
                    "LON_CTR",
                    "EGLL",
                    "N053.03.32.931",
                    "W001.00.00.000",
                    "60",
                    "abc",
                    "-1.0",
                    "10",
                })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorForInvalidMagvar()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] {
                    "UK (EGTT and EGPX) {VERSION}",
                    "LON_CTR",
                    "EGLL",
                    "N053.03.32.931",
                    "W001.00.00.000",
                    "60",
                    "36.06",
                    "abc",
                    "10",
                })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorForInvalidScale()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] {
                    "UK (EGTT and EGPX) {VERSION}",
                    "LON_CTR",
                    "EGLL",
                    "N053.03.32.931",
                    "W001.00.00.000",
                    "60",
                    "36.06",
                    "-1.0",
                    "abc",
                })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }
    }
}
