using Compiler.Event;
using Xunit;

namespace CompilerTest.Event
{
    public class CompilationStartedEventTest
    {
        [Fact]
        public void TestFormat()
        {
            var version = typeof(ComplilationStartedEvent).Assembly.GetName().Version;
            string versionString = $"{version!.Major}.{version.Minor}.{version.Build}";
            
            ComplilationStartedEvent eventObject = new();
            Assert.Equal(
                "Sector File Compiler version " + versionString + ": Starting compilation",
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
