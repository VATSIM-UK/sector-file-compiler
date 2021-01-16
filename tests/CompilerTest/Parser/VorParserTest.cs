using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class VorParserTest: AbstractParserTestCase
    {
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
            this.RunParserOnLines(lines);

            Assert.Empty(this.sectorElementCollection.Vors);
            this.logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsVorData()
        {
            this.RunParserOnLines(new List<string>(new[] { "BHD 112.050 N050.57.00.000 W001.21.24.490;comment" }));

            Vor result = this.sectorElementCollection.Vors[0];
            Assert.Equal("BHD", result.Identifier);
            Assert.Equal("112.050", result.Frequency);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Coordinate);
            this.AssertExpectedMetadata(result);
        }

        [Fact]
        public void TestItAddsVorDataTwoLetterIdentifier()
        {
            this.RunParserOnLines(new List<string>(new[] { "BH 112.050 N050.57.00.000 W001.21.24.490;comment" }));

            Vor result = this.sectorElementCollection.Vors[0];
            Assert.Equal("BH", result.Identifier);
            Assert.Equal("112.050", result.Frequency);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Coordinate);
            this.AssertExpectedMetadata(result);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.SCT_VORS;
        }
    }
}
