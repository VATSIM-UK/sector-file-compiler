using Compiler.Input.Event;
using Compiler.Input.Rule;
using Xunit;

namespace CompilerTest.Input.Event
{
    public class FilesetEmptyWarningTest
    {
        private readonly FilesetEmptyWarning compilerEvent;

        public FilesetEmptyWarningTest()
        {
            compilerEvent = new FilesetEmptyWarning(new RuleDescriptor("foo"));
        }

        [Fact]
        public void ItIsNotFatal()
        {
            Assert.False(compilerEvent.IsFatal());
        }
        
        [Fact]
        public void ItHasAMessage()
        {
            Assert.Equal("WARN: Fileset is empty for include rule: foo", compilerEvent.GetMessage());
        }
    }
}
