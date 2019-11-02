using System;
using Compiler.Transformer;
using Compiler.Argument;
using Xunit;

namespace CompilerTest.Transformer
{
    public class SystemTokenFactoryTest
    {
        private readonly CompilerArguments arguments;

        public SystemTokenFactoryTest()
        {
            this.arguments = new CompilerArguments();
            this.arguments.BuildVersion = "2020/01";
        }

        [Fact]
        public void TestItSetsYearToken()
        {
            Assert.Equal(
                DateTime.Now.Year.ToString(),
                SystemTokensFactory.GetSystemTokens(this.arguments)["{YEAR}"]
            );
        }

        [Fact]
        public void TestItSetsVersionTokenFromArguments()
        {
            Assert.Equal(
                "2020/01",
                SystemTokensFactory.GetSystemTokens(this.arguments)["{VERSION}"]
            );
        }

        [Fact]
        public void TestItSetsBuildDateTokenFromArguments()
        {
            Assert.Equal(
                DateTime.Now.ToString("yyyy-MM-dd"),
                SystemTokensFactory.GetSystemTokens(this.arguments)["{BUILD}"]
            );
        }
    }
}
