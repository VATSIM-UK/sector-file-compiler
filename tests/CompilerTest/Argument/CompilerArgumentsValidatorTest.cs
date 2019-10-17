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

        public CompilerArgumentsValidatorTest()
        {
            this.validator = new CompilerArgumentsValidator(
                new Mock<ILoggerInterface>().Object
            );
            this.arguments = new CompilerArguments();
        }

        [Fact]
        public void TestItReturnsFalseOnMissingConfigFile()
        {
            var mock = new Mock<IFileInterface>();
            mock.Setup(file => file.Exists()).Returns(false);
            this.arguments.ConfigFile = mock.Object;

            Assert.False(this.validator.Validate(this.arguments));
        }

        [Fact]
        public void TestItReturnsTrueOnValidArguments()
        {
            var mock = new Mock<IFileInterface>();
            mock.Setup(file => file.Exists()).Returns(true);
            this.arguments.ConfigFile = mock.Object;

            Assert.True(this.validator.Validate(this.arguments));
        }
    }
}
