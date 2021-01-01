using Xunit;
using Moq;
using System.IO;
using Compiler.Argument;
using Compiler.Event;
using Compiler.Error;
using Compiler.Output;

namespace CompilerTest.Argument
{
    public class CompilerArgumentsValidatorTest
    {
        private readonly CompilerArguments arguments;

        private readonly string mockConfigFile;

        private readonly Mock<IEventLogger> eventLogger;

        private readonly Mock<TextWriter> mockOutputWriter;

        public CompilerArgumentsValidatorTest()
        {
            this.mockOutputWriter = new Mock<TextWriter>();
            this.eventLogger = new Mock<IEventLogger>();
            this.arguments = new CompilerArguments();
            this.mockConfigFile = "/foo/bar";
        }

        [Fact]
        public void TestItSendsErrorEventOnMissingConfigFile()
        {
            this.arguments.OutputFiles.Add(new RwyOutput(this.mockOutputWriter.Object));
            this.arguments.OutputFiles.Add(new RwyOutput(this.mockOutputWriter.Object));
            CompilerArgumentsValidator.Validate(this.eventLogger.Object, this.arguments);
            string expectedMessage = "No config files specified";
            this.eventLogger.Verify(
                foo => foo.AddEvent(It.Is<CompilerArgumentError>(arg => arg.GetMessage().Contains(expectedMessage)))
            );
        }

        [Fact]
        public void TestItSendsErrorEventOnNoOutputFiles()
        {
            this.arguments.ConfigFiles.Add(mockConfigFile);
            CompilerArgumentsValidator.Validate(this.eventLogger.Object, this.arguments);

            string expectedMessage = "No output files specified";
            this.eventLogger.Verify(
                foo => foo.AddEvent(It.Is<CompilerArgumentError>(arg => arg.GetMessage().Contains(expectedMessage)))
            );
        }

        [Fact]
        public void TestItSetsNoErrorsOnValidConfig()
        {
            this.arguments.OutputFiles.Add(new RwyOutput(this.mockOutputWriter.Object));
            this.arguments.OutputFiles.Add(new RwyOutput(this.mockOutputWriter.Object));
            this.arguments.ConfigFiles.Add(mockConfigFile);
            CompilerArgumentsValidator.Validate(this.eventLogger.Object, this.arguments);
            this.eventLogger.VerifyNoOtherCalls();
        }
    }
}
