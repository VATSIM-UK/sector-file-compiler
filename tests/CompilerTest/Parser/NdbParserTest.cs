using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class NdbParserTest: AbstractParserTestCase
    {
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
            RunParserOnLines(lines);

            Assert.Empty(sectorElementCollection.Ndbs);
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsNdbData()
        {
            RunParserOnLines(new List<string>(new[] { "CDF 388.500 N050.57.00.000 W001.21.24.490;comment" }));

            Ndb result = sectorElementCollection.Ndbs[0];
            Assert.Equal("CDF", result.Identifier);
            Assert.Equal("388.500", result.Frequency);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Coordinate);
            AssertExpectedMetadata(result);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.SCT_NDBS;
        }
    }
}
