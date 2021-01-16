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
            this.RunParserOnLines(lines);

            Assert.Empty(this.sectorElementCollection.Colours);
            this.logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsColourData()
        {
            this.RunParserOnLines(new List<string>(new[] {"#define abc 255 ;comment"}));

            Colour result = this.sectorElementCollection.Colours[0];
            Assert.Equal("abc", result.Name);
            Assert.Equal(255, result.Value);
            this.AssertExpectedMetadata(result);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.SCT_COLOUR_DEFINITIONS;
        }
    }
}
