using System.Collections.Generic;
using Compiler.Event;
using Compiler.Input.Event;
using Compiler.Input.Rule;
using Compiler.Input.Validator;
using Moq;
using Xunit;

namespace CompilerTest.Input.Validator
{
    public class FileListNotEmptyTest
    {
        private readonly Mock<IEventLogger> log = new();

        [Fact]
        public void ItValidatesIfFileListNotEmpty()
        {
            new FilelistNotEmpty(false).Validate(new List<string>{"Foo.txt"}, new RuleDescriptor("Foo"), log.Object);
            log.Verify(foo => foo.AddEvent(It.IsAny<ICompilerEvent>()), Times.Never);
        }
        
        [Fact]
        public void ItValidatesIfFileListEmpty()
        {
            new FilelistNotEmpty(false).Validate(new List<string>(), new RuleDescriptor("Foo"), log.Object);
            log.Verify(foo => foo.AddEvent(It.IsAny<FilesetEmptyWarning>()), Times.Once);
        }
        
        [Fact]
        public void ItValidatesWithAnErrorLevelIfConfigured()
        {
            new FilelistNotEmpty(true).Validate(new List<string>(), new RuleDescriptor("Foo"), log.Object);
            log.Verify(foo => foo.AddEvent(It.IsAny<FilesetEmptyError>()), Times.Once);
        }
    }
}
