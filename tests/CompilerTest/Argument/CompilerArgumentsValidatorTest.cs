using Xunit;
using Moq;
using Compiler.Argument;
using Compiler.Output;
using Compiler.Input;
using System.IO;

namespace CompilerTest.Argument
{
    public class CompilerArgumentsValidatorTest
    {
        private readonly CompilerArgumentsValidator validator;

        private readonly CompilerArguments arguments;

        private readonly Mock<IFileInterface> mockConfigFile;

        public CompilerArgumentsValidatorTest()
        {
            this.mockConfigFile = new Mock<IFileInterface>();
            this.validator = new CompilerArgumentsValidator(
                new Mock<ILoggerInterface>().Object
            );
            this.arguments = new CompilerArguments();
            this.arguments.ConfigFile = mockConfigFile.Object;
        }

        [Fact]
        public void TestItReturnsFalseOnMissingConfigFile()
        {
            this.mockConfigFile.Setup(file => file.Exists()).Returns(false);
            Assert.False(this.validator.Validate(this.arguments));
        }

        [Fact]
        public void TestItReturnsFalseOnInvalidJsonInConfigFile()
        {
            this.mockConfigFile.Setup(file => file.Exists()).Returns(true);
            this.mockConfigFile.Setup(file => file.Contents()).Returns("{]");
            Assert.False(this.validator.Validate(this.arguments));
        }

        [Fact]
        public void TestItReturnsTrueOnValidArguments()
        {
            this.mockConfigFile.Setup(file => file.Exists()).Returns(true);
            this.mockConfigFile.Setup(file => file.Contents()).Returns("{}");
            Assert.True(this.validator.Validate(this.arguments));
        }
    }
}
