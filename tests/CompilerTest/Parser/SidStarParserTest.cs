using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class SidStarParserTest: AbstractParserTestCase
    {
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "abc:def:ghi"
            }}, // Too few segments
            new object[] { new List<string>{
                "abc:def:ghi:jkl:mno:pqr:stu"
            }}, // Too many segments
            new object[] { new List<string>{
                "abc:def:ghi:jkl:mno"
            }}, // Unknown type
        };
        
        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            RunParserOnLines(lines);

            Assert.Empty(sectorElementCollection.Runways);
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsSidStarData()
        {
            RunParserOnLines(new List<string>(new[] { "SID:EGKK:26L:ADMAG2X:FIX1 FIX2 ;comment" }));

            SidStar result = sectorElementCollection.SidStars[0];
            Assert.Equal("SID", result.Type);
            Assert.Equal("EGKK", result.Airport);
            Assert.Equal("26L", result.Runway);
            Assert.Equal("ADMAG2X", result.Identifier);
            Assert.Equal(new List<string>(new[] { "FIX1", "FIX2" }), result.Route);
            AssertExpectedMetadata(result);
        }
        
        [Fact]
        public void TestItAddsSidStarWithNoRoute()
        {
            RunParserOnLines(new List<string>(new[] { "SID:EGKK:26L:ADMAG2X: ;comment" }));

            SidStar result = sectorElementCollection.SidStars[0];
            Assert.Equal("SID", result.Type);
            Assert.Equal("EGKK", result.Airport);
            Assert.Equal("26L", result.Runway);
            Assert.Equal("ADMAG2X", result.Identifier);
            Assert.Empty(result.Route);
            AssertExpectedMetadata(result);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.ESE_SIDS;
        }
    }
}
