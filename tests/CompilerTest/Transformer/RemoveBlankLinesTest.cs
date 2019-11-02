using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Compiler.Transformer;

namespace CompilerTest.Transformer
{
    public class RemoveBlankLinesTest
    {
        private readonly RemoveBlankLines transformer;

        public RemoveBlankLinesTest()
        {
            this.transformer = new RemoveBlankLines();
        }

        [Fact]
        public void TestItDoesntRemoveNonBlankLines()
        {
            List<string> lines = new List<string>(new string[] { "a\r\n", "b\r\n", "c\r\n" });
            Assert.Equal(lines, this.transformer.Transform(lines));
        }

        [Fact]
        public void TestItRemovesJustNewlines()
        {
            List<string> lines = new List<string>(new string[] { "\r\n", "\r\n", "\r\n" });
            Assert.Equal(new List<string>(), this.transformer.Transform(lines));
        }

        [Fact]
        public void TestItStripsSpacesToRemoveLines()
        {
            List<string> lines = new List<string>(new string[] { " \r\n", "\r\n ", " \r\n " });
            Assert.Equal(new List<string>(), this.transformer.Transform(lines));
        }
    }
}
