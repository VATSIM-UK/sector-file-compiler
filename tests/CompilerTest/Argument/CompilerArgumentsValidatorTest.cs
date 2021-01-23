using Xunit;
using Moq;
using Compiler.Argument;
using Compiler.Event;
using Compiler.Error;

namespace CompilerTest.Argument
{
    public class CompilerArgumentsValidatorTest
    {
        private readonly CompilerArguments arguments;

        private readonly string mockConfigFile;

        private readonly Mock<IEventLogger> eventLogger;

        public CompilerArgumentsValidatorTest()
        {
            eventLogger = new Mock<IEventLogger>();
            arguments = new CompilerArguments();
            mockConfigFile = "/foo/bar";
        }

        [Fact]
        public void TestItSendsErrorEventOnMissingConfigFile()
        {
            CompilerArgumentsValidator.Validate(this.eventLogger.Object, this.arguments);
            string expectedMessage = "No config files specified";
            eventLogger.Verify(
                foo => foo.AddEvent(It.Is<CompilerArgumentError>(arg => arg.GetMessage().Contains(expectedMessage)))
            );
        }

        [Fact]
        public void TestItSetsNoErrorsOnValidConfig()
        {
            arguments.ConfigFiles.Add(mockConfigFile);
            CompilerArgumentsValidator.Validate(this.eventLogger.Object, this.arguments);
            eventLogger.VerifyNoOtherCalls();
        }
    }
}
