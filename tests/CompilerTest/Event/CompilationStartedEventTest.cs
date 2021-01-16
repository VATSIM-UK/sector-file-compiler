using Compiler.Event;
using Compiler.Argument;
using Xunit;

namespace CompilerTest.Event
{
    public class CompilationStartedEventTest
    {
        [Fact]
        public void TestFormat()
        {
            ComplilationStartedEvent eventObject = new();
            Assert.Equal(
                "Sector File Compiler version " + CompilerArguments.COMPILER_VERISON + ": Starting compilation",
                eventObject.GetMessage()
            );
        }

        [Fact]
        public void TestItIsNotFatal()
        {
            ComplilationStartedEvent eventObject = new();
            Assert.False(eventObject.IsFatal());
        }
    }
}
