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
        public void TestItDoesntTouchNonBlankLines()
        {
            Assert.Equal("a\r\n", this.transformer.Transform("a\r\n"));
        }

        [Fact]
        public void TestItRemovesJustNewlines()
        {
            Assert.Equal("", this.transformer.Transform("\r\n\r\n"));
        }

        [Fact]
        public void TestItStripsSpacesToRemoveLines()
        {
            Assert.Equal("", this.transformer.Transform(" \r\n \r\n "));
        }
    }
}
