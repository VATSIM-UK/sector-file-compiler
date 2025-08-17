using Xunit;
using Compiler.Error;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Error
{
    public class ValidationRuleFailureTest
    {
        private readonly ValidationRuleFailure rule;
        private readonly Definition definition;

        public ValidationRuleFailureTest()
        {
            RouteSegment segment = RouteSegmentFactory.MakeDoublePoint();
            definition = segment.GetDefinition();
            rule = new ValidationRuleFailure("just because", segment);
        }

        [Fact]
        public void TestItIsFatal()
        {
            Assert.True(rule.IsFatal());
        }

        [Fact]
        public void TestItReturnsAMessage()
        {
            Assert.Equal($"Validation rule not met: just because, defined at {definition.Filename}:{definition.LineNumber}", rule.GetMessage());
        }
    }
}
