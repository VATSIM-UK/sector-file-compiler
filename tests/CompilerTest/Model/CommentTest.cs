using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class CommentTest
    {
        private readonly Comment comment;

        public CommentTest()
        {
            this.comment = new Comment("test comment");
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("; test comment\r\n", this.comment.Compile());
        }
    }
}
