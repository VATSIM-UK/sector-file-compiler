using Xunit;
using Compiler.Event;

namespace CompilerTest.Event
{
    public class CompliationFinishedEventTest
    {
        [Fact]
        public void TestItIsNotFatal()
        {
            CompilationFinishedEvent cfe = new CompilationFinishedEvent(true); 
            Assert.False(cfe.IsFatal());
        }

        [Fact]
        public void TestMessageSuccess()
        {
            CompilationFinishedEvent cfe = new CompilationFinishedEvent(true);
            Assert.Equal("Compilation completed successfully", cfe.GetMessage());
        }

        [Fact]
        public void TestMessageFailure()
        {
            CompilationFinishedEvent cfe = new CompilationFinishedEvent(false);
            Assert.Equal("Compilation failed", cfe.GetMessage());
        }
    }
}
