using Compiler.Argument;
using Xunit;
using Compiler.Transformer;

namespace CompilerTest.Transformer
{
    public class RemoveAllCommentsTest
    {
        private RemoveAllComments MakeTransformer(bool shouldStrip)
        {
            var arguments = CompilerArgumentsFactory.Make();
            arguments.StripComments = shouldStrip;
            return RemoveAllCommentsFactory.Make(arguments);
        }

        [Fact]
        public void TestItDoesntAlterNonCommentLines()
        {
            Assert.Equal("abc", MakeTransformer(true).Transform("abc"));
        }

        [Fact]
        public void TestItRemovesCommentsFromEndOfLines()
        {
            Assert.Equal("abc", MakeTransformer(true).Transform("abc ; comment"));
        }
        
        [Fact]
        public void TestItLeavesCommentsIfNotStripping()
        {
            Assert.Equal("abc ; comment", MakeTransformer(false).Transform("abc ; comment"));
        }

        [Fact]
        public void TestItReturnsNullIfNoDataOnLines()
        {
            Assert.Null(MakeTransformer(true).Transform(" ; comment"));
        }

        [Theory]
        [InlineData("abc ; @preserveComment comment", "abc ; comment")]
        [InlineData("abc ;  @preserveComment comment", "abc ; comment")]
        [InlineData("abc ;   @preserveComment comment", "abc ; comment")]
        [InlineData("abc ;   @preserveComment  comment", "abc ; comment")]
        [InlineData("; @preserveComment", ";")]
        public void TestItPreservesAnnotatedComments(string input, string expected)
        {
            Assert.Equal(expected, MakeTransformer(true).Transform(input));
        }
        
        [Theory]
        [InlineData("abc ; @preserveComment comment", "abc ; comment")]
        [InlineData("abc ;  @preserveComment comment", "abc ; comment")]
        [InlineData("abc ;   @preserveComment comment", "abc ; comment")]
        [InlineData("abc ;   @preserveComment  comment", "abc ; comment")]
        [InlineData("; @preserveComment", ";")]
        public void TestItRemovesPreserveAnnotationIfNotStripping(string input, string expected)
        {
            Assert.Equal(expected, MakeTransformer(false).Transform(input));
        }
    }
}
