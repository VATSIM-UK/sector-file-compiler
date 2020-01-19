using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class LabelTest
    {
        private readonly Label label;

        public LabelTest()
        {
            this.label = new Label("label 1", new Coordinate("abc", "def"), "colour", null);
        }

        [Fact]
        public void TestItSetsText()
        {
            Assert.Equal("label 1", this.label.Text);
        }

        [Fact]
        public void TestItSetsPosition()
        {
            Assert.Equal(new Coordinate("abc", "def"), this.label.Position);
        }

        [Fact]
        public void TestItSetsColour()
        {
            Assert.Equal("colour", this.label.Colour);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("\"label 1\" abc def colour\r\n", this.label.Compile());
        }

        [Fact]
        public void TestItCompilesWithComment()
        {
            Assert.Equal(
                "\"label 1\" abc def colour ;comment\r\n",
                new Label("label 1", new Coordinate("abc", "def"), "colour", "comment").Compile()
            );
        }
    }
}
