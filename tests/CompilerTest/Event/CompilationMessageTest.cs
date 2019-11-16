using Compiler.Event;
using Xunit;

namespace CompilerTest.Event
{
    public class CompilationMessageTest
    {
        private readonly CompilationMessage message;

        public CompilationMessageTest()
        {
            this.message = new CompilationMessage("This is a test message");
        }

        [Fact]
        public void TestItIsNonFatal()
        {
            Assert.False(this.message.IsFatal());
        }

        [Fact]
        public void TestOutput()
        {
            Assert.Equal("INFO: This is a test message", this.message.GetMessage());
        }
    }
}
