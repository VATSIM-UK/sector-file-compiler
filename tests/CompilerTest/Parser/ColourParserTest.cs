using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class ColourParserTest: AbstractParserTestCase
    {
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "#define abc"
            }}, // Too few segments
            new object[] { new List<string>{
                "#nodefine abc 123"
            }}, // Not started with define
            new object[] { new List<string>{
                "#define abc 123.4"
            }}, // Value is a float
            new object[] { new List<string>{
                "#define abc 123abc"
            }}, // Value is a string
        };

        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            RunParserOnLines(lines);

            Assert.Empty(sectorElementCollection.Colours);
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsColourData()
        {
            RunParserOnLines(new List<string>(new[] {"#define abc 255 ;comment"}));

            Colour result = sectorElementCollection.Colours[0];
            Assert.Equal("abc", result.Name);
            Assert.Equal(255, result.Value);
            AssertExpectedMetadata(result);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.SCT_COLOUR_DEFINITIONS;
        }
    }
}
