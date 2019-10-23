using Xunit;
using Compiler.Parser;

namespace CompilerTest.Parser
{
    public class LineCommentParserTest
    {
        [Fact]
        public void TestItReturnsEmptyStringIfNoComment()
        {
            Assert.Equal("", LineCommentParser.ParseComment("aline withno comment  "));
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
    }
}
