using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class InfoCoordinateTest : AbstractModelTestCase
    {
        private readonly InfoCoordinate model;

        public InfoCoordinateTest()
        {
            this.model = new InfoCoordinate(
                new Coordinate("abc", "def"),
                this.GetDefinition(),
                this.GetDocbock(),
                this.GetInlineComment()
            );
        }

        [Fact]
        public void TestItSetsCoordinate()
        {
            Assert.Equal(new Coordinate("abc", "def"), this.model.Coordinate);
        }

        [Fact]
        public void TestItSetsDefinition()
        {
            Assert.Equal(this.GetDefinition(), this.model.GetDefinition());
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "abc def",
                this.model.GetCompileData()
            );
        }
    }
}