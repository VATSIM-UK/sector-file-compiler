using System;
using Compiler.Argument;
using Xunit;
using Compiler.Transformer;

namespace CompilerTest.Transformer
{
    public class ReplaceTokensTest
    {
        private readonly ReplaceTokens transformer;

        public ReplaceTokensTest()
        {
            var arguments = CompilerArgumentsFactory.Make();
            arguments.TokenReplacers.Add(new TokenDateReplacer("{TOKENA}", "Y-m-d"));
            arguments.TokenReplacers.Add(new TokenDateReplacer("{TOKENB}", "d-m-Y"));
            transformer = ReplaceTokensFactory.Make(arguments);
        }

        [Fact]
        public void TestItReplacesTokensInLines()
        {
            string input = "A line with {TOKENA} and {TOKENB} in it";
            string expected = $"A line with {DateTime.Now:Y-m-d} and {DateTime.Now:d-m-Y} in it";
            Assert.Equal(expected, this.transformer.Transform(input));
        }
    }
}
