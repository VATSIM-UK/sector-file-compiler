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
            List<string> inputLines = new List<string>(new string[] { "A line with {TOKENA}", "A line with {TOKENB} in it"});
            List<string> expectedLines = new List<string>(new string[] { "A line with VALUEA", "A line with VALUEB in it"});
            Assert.Equal(expectedLines, this.transformer.Transform(inputLines));
        }
    }
}
