using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class CommentLineTest
    {
        private readonly CommentLine comment;

        public CommentLineTest()
        {
            this.comment = new CommentLine("test comment");
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("; test comment\r\n", this.comment.Compile());
        }
    }
}
