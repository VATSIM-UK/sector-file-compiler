using Xunit;
using Compiler.Parser;

namespace CompilerTest.Parser
{
    public class LineCommentParserTest
    {
        [Fact]
        public void TestItReturnsNullIfNoComment()
        {
            Assert.Null(LineCommentParser.ParseComment("aline withno comment  "));
        }

        [Fact]
        public void TestItParsesLineComment()
        {
            Assert.Equal("comment", LineCommentParser.ParseComment("aline witha ;comment"));
        }

        [Fact]
        public void TestItStripsWhitespaceFromComment()
        {
            Assert.Equal("comment", LineCommentParser.ParseComment("aline witha ;   comment  "));
        }

        [Fact]
        public void TestItReturnsNullIfNoData()
        {
            Assert.Equal("", LineCommentParser.ParseData(";comment "));
        }

        [Fact]
        public void TestItParsesData()
        {
            Assert.Equal("abc", LineCommentParser.ParseData("abc;comment "));
        }

        [Fact]
        public void TestItStripsWhitespaceFromData()
        {
            Assert.Equal("abc", LineCommentParser.ParseData("abc   ;comment "));
        }
    }
}
