using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class LabelParserTest: AbstractParserTestCase
    {
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "\"test label\" N050.57.00.000 W001.21.24.490"
            }}, // Bad number of segments
            new object[] { new List<string>{
                "\"test label\" N050.57.00.000 N001.21.24.490 red"
            }}, // Bad coordinate
            new object[] { new List<string>{
                "\"test label\"\" N050.57.00.000 N001.21.24.490 red"
            }}, // Too many quotes
            new object[] { new List<string>{
                "abc\"test label\" N050.57.00.000 N001.21.24.490 red"
            }}, // Doesnt start with quotes
        };
        
        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            RunParserOnLines(lines);

            Assert.Empty(sectorElementCollection.Labels);
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsLabelData()
        {
            RunParserOnLines(new List<string>(new[] { "\"test label\" N050.57.00.000 W001.21.24.490 red ;comment" }));

            Label result = sectorElementCollection.Labels[0];
            Assert.Equal("test label", result.Text);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Position);
            Assert.Equal("red", result.Colour);
            AssertExpectedMetadata(result);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.SCT_LABELS;
        }
    }
}
