using Xunit;
using Compiler.Error;

namespace CompilerTest.Error
{
    public class SyntaxErrorTest
    {
        private readonly SyntaxError error;
        public SyntaxErrorTest()
        {
            this.error = new SyntaxError("Fooproblem", "Foofile", 5);
        }

        [Fact]
        public void TestItIsFatal()
        {
            Assert.True(error.IsFatal());
        }

        [Fact]
        public void TestItHasAMessage()
        {
            Assert.Equal("Syntax Error: Fooproblem in Foofile at position 5", this.error.GetMessage());
        }
    }
}
