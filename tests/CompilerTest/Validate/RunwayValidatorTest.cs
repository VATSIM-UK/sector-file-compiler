using Xunit;
using Compiler.Validate;

namespace CompilerTest.Validate
{
    public class RunwayValidatorTest
    {
        [Theory]
        [InlineData("00")]
        [InlineData("01B")]
        [InlineData("37")]
        [InlineData("36c")]
        [InlineData("")]
        public void TestValidationFailure(string input)
        {
            Assert.False(RunwayValidator.RunwayValid(input));
        }

        [Theory]
        [InlineData("01")]
        [InlineData("23R")]
        [InlineData("36C")]
        [InlineData("18")]
        [InlineData("09")]
        public void TestValidationSuccess(string input)
        {
            Assert.True(RunwayValidator.RunwayValid(input));
        }

        [Theory]
        [InlineData("01B")]
        [InlineData("37")]
        [InlineData("36c")]
        [InlineData("")]
        public void TestValidationFailureWithAdjacent(string input)
        {
            Assert.False(RunwayValidator.RunwayValidIncludingAdjacent(input));
        }

        [Theory]
        [InlineData("00")]
        [InlineData("01")]
        [InlineData("23R")]
        [InlineData("36C")]
        [InlineData("18")]
        [InlineData("09")]
        public void TestValidationSuccessWithAdjacent(string input)
        {
            Assert.True(RunwayValidator.RunwayValidIncludingAdjacent(input));
        }
    }
}
