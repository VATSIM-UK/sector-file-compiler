using System;
using Compiler.Transformer;
using Xunit;

namespace CompilerTest.Transformer
{
    public class TokenDateReplacerTest
    {
        [Fact]
        public void TestItReplacesTokens()
        {
            TokenDateReplacer replacer = new("{DATE}", "Y-m-d");
            Assert.Equal(
                $"This string has a {DateTime.Now:Y-m-d} token in it",
                replacer.ReplaceTokens("This string has a {DATE} token in it")
            );
        }
    }
}