using Xunit;
using Compiler.Validate;

namespace CompilerTest.Validate
{
    public class SquawkValidatorTest
    {
        [Theory]
        [InlineData("0")]
        [InlineData("00")]
        [InlineData("000")]
        [InlineData("00000")]
        [InlineData("1358")]
        [InlineData("8351")]
        [InlineData("1851")]
        [InlineData("1381")]
        [InlineData("abcd")]
        public void TestValidationFailure(string input)
        {
            Assert.False(SquawkValidator.SquawkValid(input));
        }

        [Theory]
        [InlineData("0000")]
        [InlineData("7777")]
        [InlineData("1234")]
        [InlineData("3241")]
        [InlineData("7657")]
        [InlineData("7653")]
        [InlineData("0017")]
        public void TestValidationSuccess(string input)
        {
            Assert.True(SquawkValidator.SquawkValid(input));
        }
    }
}
