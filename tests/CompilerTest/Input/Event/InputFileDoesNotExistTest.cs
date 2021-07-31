using Compiler.Input.Event;
using Compiler.Input.Rule;
using Xunit;

namespace CompilerTest.Input.Event
{
    public class InputFileDoesNotExistTest
    {
        private readonly InputFileDoesNotExist compilerEvent;

        public InputFileDoesNotExistTest()
        {
            compilerEvent = new InputFileDoesNotExist("Foo.txt", new RuleDescriptor("foo"));
        }

        [Fact]
        public void ItIsFatal()
        {
            Assert.True(compilerEvent.IsFatal());
        }
        
        [Fact]
        public void ItHasAMessage()
        {
            Assert.Equal("Input file Foo.txt does not exist for inclusion rule: foo", compilerEvent.GetMessage());
        }
    }
}
