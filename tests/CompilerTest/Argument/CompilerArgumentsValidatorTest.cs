using Xunit;
using Moq;
using System.IO;
using Compiler.Argument;
using Compiler.Event;
using Compiler.Error;

namespace CompilerTest.Argument
{
    public class CompilerArgumentsValidatorTest
    {
        private readonly CompilerArguments arguments;

        private readonly Mock<IFileInterface> mockConfigFile;

        private readonly Mock<IEventLogger> eventLogger;

        private readonly Mock<TextWriter> mockOutputs;

        public CompilerArgumentsValidatorTest()
        {
            this.mockConfigFile = new Mock<IFileInterface>();
            this.mockOutputs = new Mock<TextWriter>();
            this.eventLogger = new Mock<IEventLogger>();
            this.arguments = new CompilerArguments();
            this.arguments.ConfigFiles.Add(mockConfigFile.Object);
            this.mockConfigFile.Setup(foo => foo.GetPath()).Returns("/foo/bar");
            this.arguments.OutFileEse = this.mockOutputs.Object;
            this.arguments.OutFileSct = this.mockOutputs.Object;
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
        public void TestItSendsErrorEventOnConfigFileNotFound()
        {
            this.mockConfigFile.Setup(file => file.Exists()).Returns(false);
            this.mockConfigFile.Setup(file => file.GetPath()).Returns("foo/bar/baz.txt");
            CompilerArgumentsValidator.Validate(this.eventLogger.Object, this.arguments);

            string expectedMessage = "The configuration file could not be found: foo/bar/baz.txt";
            this.eventLogger.Verify(
                foo => foo.AddEvent(It.Is<CompilerArgumentError>(arg => arg.GetMessage().Contains(expectedMessage)))
            );
        }

        [Fact]
        public void TestItSendsErrorEventOnEseOutputNotSet()
        {
            this.arguments.OutFileEse = null;
            this.mockConfigFile.Setup(file => file.Exists()).Returns(true);
            this.mockConfigFile.Setup(file => file.Contents()).Returns("{]");
            CompilerArgumentsValidator.Validate(this.eventLogger.Object, this.arguments);

            string expectedMessage = "ESE output file path must be specified";
            this.eventLogger.Verify(
                foo => foo.AddEvent(It.Is<CompilerArgumentError>(arg => arg.GetMessage().Contains(expectedMessage)))
            );
        }

        [Fact]
        public void TestItSendsErrorEventOnSctOutputNotSet()
        {
            this.arguments.OutFileSct = null;
            this.mockConfigFile.Setup(file => file.Exists()).Returns(true);
            this.mockConfigFile.Setup(file => file.Contents()).Returns("{]");
            CompilerArgumentsValidator.Validate(this.eventLogger.Object, this.arguments);

            string expectedMessage = "SCT output file path must be specified";
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
