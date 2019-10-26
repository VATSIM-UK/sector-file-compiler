using Xunit;
using Compiler.Error;

namespace CompilerTest.Error
{
    public class CompilerArgumentErrorTest
    {
        private readonly CompilerArgumentError error;
        public CompilerArgumentErrorTest()
        {
            this.error = new CompilerArgumentError("Fooproblem");
        }

        [Fact]
        public void TestItIsFatal()
        {
            Assert.True(error.IsFatal());
        }

        [Fact]
        public void TestItHasAMessage()
        {
            Assert.Equal("Argument error: Fooproblem", this.error.GetMessage());
        }
    }
}
