using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class ActiveRunwayParserTest: AbstractParserTestCase
    {
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{ "ACTIVE_RUNWAY:EGHI:20:1:EXTRA ;comment" }}, // Too many segments
            new object[] { new List<string>{ "ACTIVE_RUNWAY:EGHI:20 ;comment" }}, // Too few segments
            new object[] { new List<string>{ "ACTIVE_RUNWAYS:EGHI:20:1 ;comment" } }, // Bad declaration
            new object[] { new List<string>{ "ACTIVE_RUNWAY:ASD1:20:1 ;comment" } }, // Invalid icao
            new object[] { new List<string>{ "ACTIVE_RUNWAY:EGHI:37:1 ;comment" } }, // Invalid runway
            new object[] { new List<string>{ "ACTIVE_RUNWAY:EGHI:20:2 ;comment" } }, // Invalid mode
        };

        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            this.RunParserOnLines(lines);

            Assert.Empty(this.sectorElementCollection.ActiveRunways);
            this.logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsDataInMode0()
        {
            this.RunParserOnLines(new List<string>(new string[] { "ACTIVE_RUNWAY:EGHI:20:0 ;comment" }));

            ActiveRunway result = this.sectorElementCollection.ActiveRunways[0];
            Assert.Equal("EGHI", result.Airfield);
            Assert.Equal("20", result.Identifier);
            Assert.Equal(0, result.Mode);
            this.AssertExpectedMetadata(result, 1);
        }

        [Fact]
        public void TestItAddsDataInMode1()
        {
            this.RunParserOnLines(new List<string>(new string[] { "ACTIVE_RUNWAY:EGHI:20:1 ;comment" }));

            ActiveRunway result = this.sectorElementCollection.ActiveRunways[0];
            Assert.Equal("EGHI", result.Airfield);
            Assert.Equal("20", result.Identifier);
            Assert.Equal(1, result.Mode);
            this.AssertExpectedMetadata(result, 1);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.RWY_ACTIVE_RUNWAY;
        }
    }
}
