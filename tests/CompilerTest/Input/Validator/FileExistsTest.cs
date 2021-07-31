using Compiler.Event;
using Compiler.Input.Event;
using Compiler.Input.Rule;
using Compiler.Input.Validator;
using Moq;
using Xunit;

namespace CompilerTest.Input.Validator
{
    public class FileExistsTest
    {
        private readonly FileExists validator;
        private readonly Mock<IEventLogger> log = new();

        public FileExistsTest()
        {
            validator = new FileExists();
        }

        [Fact]
        public void ItValidatesTrueIfFileExists()
        {
            Assert.True(
                validator.Validate("_TestData/FileExistsValidator/Foo.txt", new RuleDescriptor("Foo"), log.Object)
            );
            log.Verify(foo => foo.AddEvent(It.IsAny<ICompilerEvent>()), Times.Never);
        }
        
        [Fact]
        public void ItValidatesFalseIfFileDoesntExist()
        {
            Assert.False(
                validator.Validate("_TestData/FileExistsValidator/Bar.txt", new RuleDescriptor("Foo"), log.Object)
            );
            log.Verify(foo => foo.AddEvent(It.IsAny<InputFileDoesNotExist>()), Times.Once);
        }
    }
}
