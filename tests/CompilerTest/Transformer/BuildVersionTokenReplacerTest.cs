using Compiler.Argument;
using Compiler.Transformer;
using Xunit;

namespace CompilerTest.Transformer
{
    public class BuildVersionTokenReplacerTest
    {
        [Fact]
        public void TestItReplacesTokens()
        {
            string version = "This is the {VERSION} string";
            var replacer = BuildVersionTokenReplacerFactory.Make(CompilerArgumentsFactory.Make());
            Assert.Equal(
                "This is the VERSION string",
                replacer.Transform(version)
            );
        }
    }
}