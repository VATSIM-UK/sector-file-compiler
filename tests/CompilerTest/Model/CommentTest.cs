using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class CommentTest
    {
        [Fact]
        public void TestItConvertsToStringWhenEmpty()
        {
            Assert.Equal("", new Comment("").ToString());
        }
        
        [Fact]
        public void TestItConvertsToString()
        {
            Assert.Equal("; foo bar", new Comment("foo bar").ToString());
        }
    }
}
