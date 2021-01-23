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

        [Theory]
        [InlineData("abc ; @preserveComment comment", "abc ; comment")]
        [InlineData("abc ;  @preserveComment comment", "abc ; comment")]
        [InlineData("abc ;   @preserveComment comment", "abc ; comment")]
        [InlineData("abc ;   @preserveComment  comment", "abc ; comment")]
        [InlineData("; @preserveComment", ";")]
        public void TestItPreservesAnnotatedComments(string input, string expected)
        {
            Assert.Equal(expected, transformer.Transform(input));
        }
    }
}
