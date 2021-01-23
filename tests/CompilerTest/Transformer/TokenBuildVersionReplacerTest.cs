using Compiler.Argument;
using Compiler.Transformer;
using Xunit;

namespace CompilerTest.Transformer
{
    public class TokenBuildVersionReplacerTest
    {
        [Fact]
        public void TestItReplacesTokens()
        {
            var arguments = CompilerArgumentsFactory.Make();
            string version = "This is the {VERSION} string";
            var replacer = new TokenBuildVersionReplacer(arguments, "{VERSION}");
            Assert.Equal(
                "This is the BUILD_VERSION string",
                replacer.ReplaceTokens(version)
            );
        }
    }
}