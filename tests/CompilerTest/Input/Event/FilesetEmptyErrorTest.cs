using Compiler.Input.Event;
using Compiler.Input.Rule;
using Xunit;

namespace CompilerTest.Input.Event
{
    public class FilesetEmptyErrorTest
    {
        private readonly FilesetEmptyError compilerEvent;

        public FilesetEmptyErrorTest()
        {
            compilerEvent = new FilesetEmptyError(new RuleDescriptor("foo"));
        }

        [Fact]
        public void ItIsFatal()
        {
            Assert.True(compilerEvent.IsFatal());
        }
        
        [Fact]
        public void ItHasAMessage()
        {
            Assert.Equal("ERROR: Fileset is empty for include rule: foo", compilerEvent.GetMessage());
        }
    }
}
