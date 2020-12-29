using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class InfoParserTest: AbstractParserTestCase
    {
        [Fact]
        public void TestItAddsInfoData()
        {
            this.RunParserOnLines(new List<string>(new string[] {
                "UK (EGTT and EGPX) {VERSION}",
                "LON_CTR",
                "EGLL",
                "N053.03.32.931",
                "W001.00.00.000",
                "60",
                "36.06",
                "-1.0",
                "10",
            }));

            Info result = this.sectorElementCollection.Info;
            Assert.Equal("UK (EGTT and EGPX) {VERSION}", result.Name.Name);
            Assert.Equal("LON_CTR", result.Callsign.Callsign);
            Assert.Equal("EGLL", result.Airport.AirportIcao);
            Assert.Equal("N053.03.32.931", result.Latitude.Latitude);
            Assert.Equal("W001.00.00.000", result.Longitude.Longitude);
            Assert.Equal(60, result.MilesPerDegreeLatitude.Miles);
            Assert.Equal(36.06, result.MilesPerDegreeLongitude.Miles);
            Assert.Equal(-1.0, result.MagneticVariation.Variation);
            Assert.Equal(10, result.Scale.Scale);
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
            this.RunParserOnLines(lines);

            Assert.Null(this.sectorElementCollection.Info);
            this.logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.SCT_INFO;
        }
    }
}
