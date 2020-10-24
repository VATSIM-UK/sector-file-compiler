using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class InfoLongitudeTest : AbstractModelTestCase
    {
        private readonly InfoLongitude model;

        public InfoLongitudeTest()
        {
            this.model = new InfoLongitude(
                "def",
                this.GetDefinition(),
                this.GetDocbock(),
                this.GetInlineComment()
            );
        }

        [Fact]
        public void TestItSetsLongitude()
        {
            Assert.Equal("def", this.model.Longitude);
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
                "def",
                this.model.GetCompileData()
            );
        }
    }
}