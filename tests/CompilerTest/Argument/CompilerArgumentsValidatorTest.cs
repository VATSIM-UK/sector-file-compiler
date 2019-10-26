using Xunit;
using Moq;
using Compiler.Argument;
using Compiler.Input;
using Compiler.Event;
using Compiler.Error;

namespace CompilerTest.Argument
{
    public class CompilerArgumentsValidatorTest
    {
        private readonly CompilerArguments arguments;

        private readonly Mock<IFileInterface> mockConfigFile;

        private readonly Mock<IEventLogger> eventLogger;

        public CompilerArgumentsValidatorTest()
        {
            this.mockConfigFile = new Mock<IFileInterface>();
            this.eventLogger = new Mock<IEventLogger>();
            this.arguments = new CompilerArguments
            {
                ConfigFile = mockConfigFile.Object
            };
            this.mockConfigFile.Setup(foo => foo.GetPath()).Returns("/foo/bar");
        }

        [Fact]
        public void TestItSendsErrorEventOnMissingConfigFile()
        {
            this.mockConfigFile.Setup(file => file.Exists()).Returns(false);
            CompilerArgumentsValidator.Validate(this.eventLogger.Object, this.arguments);
            string expectedMessage = "The configuration file could not be found: /foo/bar";
            this.eventLogger.Verify(
                foo => foo.AddEvent(It.Is<CompilerArgumentError>(arg => arg.GetMessage().Contains(expectedMessage)))
            );
        }

        [Fact]
        public void TestItSendsErrorEventOnInvalidJsonInConfigFile()
        {
            this.mockConfigFile.Setup(file => file.Exists()).Returns(true);
            this.mockConfigFile.Setup(file => file.Contents()).Returns("{]");
            CompilerArgumentsValidator.Validate(this.eventLogger.Object, this.arguments);

            string expectedMessage = "The configuration file is not valid JSON";
            this.eventLogger.Verify(
                foo => foo.AddEvent(It.Is<CompilerArgumentError>(arg => arg.GetMessage().Contains(expectedMessage)))
            );
        }

        [Fact]
        public void TestItSetsNoErrorsOnValidConfig()
        {
            this.mockConfigFile.Setup(file => file.Exists()).Returns(true);
            this.mockConfigFile.Setup(file => file.Contents()).Returns("{}");
            CompilerArgumentsValidator.Validate(this.eventLogger.Object, this.arguments);
            this.eventLogger.VerifyNoOtherCalls();
        }
    }
}
