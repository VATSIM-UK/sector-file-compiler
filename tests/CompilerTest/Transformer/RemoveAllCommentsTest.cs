using Xunit;
using Compiler.Transformer;

namespace CompilerTest.Transformer
{
    public class RemoveAllCommentsTest
    {
        private readonly RemoveAllComments transformer;

        public RemoveAllCommentsTest()
        {
            transformer = new RemoveAllComments();
        }

        [Fact]
        public void TestItDoesntAlterNonCommentLines()
        {
            Assert.Equal("abc", transformer.Transform("abc"));
        }

        [Fact]
        public void TestItRemovesCommentsFromEndOfLines()
        {
            Assert.Equal("abc", transformer.Transform("abc; comment"));
        }

        [Fact]
        public void TestItReturnsNullIfNoDataOnLines()
        {
            Assert.Null(transformer.Transform(" ; comment"));
        }
    }
}
