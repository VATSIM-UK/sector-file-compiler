using Compiler.Event;
using Xunit;

namespace CompilerTest.Event
{
    public class CompilationFinishedEventTest
    {
        [Fact]
        public void TestFormatSuccessfulCompliation()
        {
            CompilationFinishedEvent eventObject = new CompilationFinishedEvent(true);
            Assert.Equal("Compilation completed successfully", eventObject.GetMessage());
        }

        [Fact]
        public void TestFormatFailedCompliation()
        {
            CompilationFinishedEvent eventObject = new CompilationFinishedEvent(false);
            Assert.Equal("Compilation failed", eventObject.GetMessage());
        }

        [Fact]
        public void TestItIsNotFatal()
        {
            CompilationFinishedEvent eventObject = new CompilationFinishedEvent(false);
            Assert.False(eventObject.IsFatal());
        }
    }
}
