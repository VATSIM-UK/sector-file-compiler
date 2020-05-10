using Xunit;
using Compiler.Error;

namespace CompilerTest.Error
{
    public class ConfigFileValidationErrorTest
    {
        private readonly ConfigFileValidationError error;
        public ConfigFileValidationErrorTest()
        {
            this.error = new ConfigFileValidationError("Fooproblem");
        }

        [Fact]
        public void TestItIsFatal()
        {
            Assert.True(error.IsFatal());
        }

        [Fact]
        public void TestItHasAMessage()
        {
            Assert.Equal("Invalid compiler configuration: Fooproblem", this.error.GetMessage());
        }
    }
}
