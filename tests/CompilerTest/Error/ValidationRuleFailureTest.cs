using Xunit;
using Compiler.Error;

namespace CompilerTest.Error
{
    public class ValidationRuleFailureTest
    {
        private readonly ValidationRuleFailure rule;

        public ValidationRuleFailureTest()
        {
            this.rule = new ValidationRuleFailure("just because");
        }

        [Fact]
        public void TestItIsFatal()
        {
            Assert.True(this.rule.IsFatal());
        }

        [Fact]
        public void TestItReturnsAMessage()
        {
            Assert.Equal("Validation rule not met: just because", this.rule.GetMessage());
        }
    }
}
