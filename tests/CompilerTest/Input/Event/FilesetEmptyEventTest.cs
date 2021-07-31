using Compiler.Input.Event;
using Compiler.Input.Rule;
using Xunit;

namespace CompilerTest.Input.Event
{
    public class FilesetEmptyEventTest
    {
        private readonly FilesetEmptyEvent compilerEvent;

        public FilesetEmptyEventTest()
        {
            compilerEvent = new FilesetEmptyEvent(new RuleDescriptor("foo"));
        }

        [Fact]
        public void ItIsNotFatal()
        {
            Assert.False(compilerEvent.IsFatal());
        }
        
        [Fact]
        public void ItHasAMessage()
        {
            Assert.Equal("Fileset is empty for include rule: foo", compilerEvent.GetMessage());
        }
    }
}
