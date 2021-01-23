using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class LabelTest
    {
        private readonly Label label;

        public LabelTest()
        {
            this.label = new Label(
                "label 1",
                new Coordinate("abc", "def"),
                "colour",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
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
            Assert.Equal("\"label 1\" abc def colour", this.label.GetCompileData(new SectorElementCollection()));
        }
    }
}
