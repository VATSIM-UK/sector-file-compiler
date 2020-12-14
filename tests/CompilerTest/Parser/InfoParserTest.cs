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
    public class InfoParserTest
    {
        private readonly InfoParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public InfoParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (InfoParser)(new DataParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSectionKeys.SCT_INFO);
        }

        [Fact]
        public void TestItAddsInfoData()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
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

        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "UK (EGTT and EGPX) {VERSION}",
                "LON_CTR",
                "EGLL",
                "N053.03.32.931",
                "W001.00.00.000",
                "60",
                "36.06",
                "-1.0",
            }}, // Not enough data
            new object[] { new List<string>{
                "UK (EGTT and EGPX) {VERSION}",
                "LON_CTR",
                "EGLL",
                "abc",
                "W001.00.00.000",
                "60",
                "36.06",
                "-1.0",
                "10",
            }}, // Invalid latitude
            new object[] { new List<string>{
                "UK (EGTT and EGPX) {VERSION}",
                "LON_CTR",
                "EGLL",
                "N053.03.32.931",
                "abc",
                "60",
                "36.06",
                "-1.0",
                "10",
            }}, // Invalid longitude
            new object[] { new List<string>{
                "UK (EGTT and EGPX) {VERSION}",
                "LON_CTR",
                "EGLL",
                "N053.03.32.931",
                "W001.00.00.000",
                "abc",
                "36.06",
                "-1.0",
                "10",
            }}, // Invalid miles per latitude
            new object[] { new List<string>{
                    "UK (EGTT and EGPX) {VERSION}",
                    "LON_CTR",
                    "EGLL",
                    "N053.03.32.931",
                    "W001.00.00.000",
                    "60",
                    "abc",
                    "-1.0",
                    "10",
            }}, // Invalid miles per longitude
            new object[] { new List<string>{
                    "UK (EGTT and EGPX) {VERSION}",
                    "LON_CTR",
                    "EGLL",
                    "N053.03.32.931",
                    "W001.00.00.000",
                    "60",
                    "36.06",
                    "abc",
                    "10",
            }}, // Invalid magvar
            new object[] { new List<string>{
                    "UK (EGTT and EGPX) {VERSION}",
                    "LON_CTR",
                    "EGLL",
                    "N053.03.32.931",
                    "W001.00.00.000",
                    "60",
                    "36.06",
                    "-1.0",
                    "abc",
            }}, // Invalid scale
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

            Assert.Null(this.collection.Info);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }
    }
}
