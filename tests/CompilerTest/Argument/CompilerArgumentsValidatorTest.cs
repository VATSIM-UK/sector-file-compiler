using Xunit;
using Moq;
using Compiler.Argument;
using Compiler.Output;
using Compiler.Input;

namespace CompilerTest.Argument
{
    public class CompilerArgumentsValidatorTest
    {
        private readonly CompilerArgumentsValidator validator;

        private readonly CompilerArguments arguments;

        private readonly Mock<IFileInterface> mockConfigFile;

        private readonly Mock<IFileInterface> mockOutputFile;

        public CompilerArgumentsValidatorTest()
        {
            this.mockConfigFile = new Mock<IFileInterface>();
            this.mockOutputFile = new Mock<IFileInterface>();
            this.validator = new CompilerArgumentsValidator(
                new Mock<ILoggerInterface>().Object
            );
            this.arguments = new CompilerArguments();
            this.arguments.ConfigFile = mockConfigFile.Object;
            this.arguments.OutFile = mockOutputFile.Object;
        }

        [Fact]
        public void TestItReturnsFalseOnMissingConfigFile()
        {
            this.mockConfigFile.Setup(file => file.Exists()).Returns(false);
            this.mockOutputFile.Setup(file => file.IsWritable()).Returns(true);
            Assert.False(this.validator.Validate(this.arguments));
        }

        [Fact]
        public void TestItReturnsFalseOnNonWritableOutputFile()
        {
            this.mockConfigFile.Setup(file => file.Exists()).Returns(true);
            this.mockOutputFile.Setup(file => file.IsWritable()).Returns(false);
            Assert.False(this.validator.Validate(this.arguments));
        }

        [Fact]
        public void TestItReturnsTrueOnValidArguments()
        {
            this.mockConfigFile.Setup(file => file.Exists()).Returns(true);
            this.mockOutputFile.Setup(file => file.IsWritable()).Returns(true);
            Assert.True(this.validator.Validate(this.arguments));
        }
    }
}
