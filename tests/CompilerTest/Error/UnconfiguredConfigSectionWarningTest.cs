using Xunit;
using Compiler.Error;

namespace CompilerTest.Error
{
    public class UnconfiguredConfigSectionWarningTest
    {
        private readonly UnconfiguredConfigSectionWarning error;
        public UnconfiguredConfigSectionWarningTest()
        {
            this.error = new UnconfiguredConfigSectionWarning("Foosection");
        }

        [Fact]
        public void TestItIsNotFatal()
        {
            Assert.False(error.IsFatal());
        }

        [Fact]
        public void TestItHasAMessage()
        {
            Assert.Equal("Unconfigured configuration section Foosection", this.error.GetMessage());
        }
    }
}
