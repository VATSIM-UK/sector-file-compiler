using System.Collections.Generic;
using Xunit;
using Compiler.Transformer;

namespace CompilerTest.Transformer
{
    public class ReplaceTokensTest
    {
        private readonly ReplaceTokens transformer;

        public ReplaceTokensTest()
        {
            this.transformer = new ReplaceTokens(
                new Dictionary<string, string>() { { "{TOKENA}", "VALUEA" }, { "{TOKENB}", "VALUEB" } }
            );
        }

        [Fact]
        public void TestItReplacesTokensInLines()
        {
            string input = "A line with {TOKENA} and {TOKENB} in it";
            string expected = "A line with VALUEA and VALUEB in it";
            Assert.Equal(expected, this.transformer.Transform(input));
        }
    }
}
