using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class InfoLatitudeTest : AbstractModelTestCase
    {
        private readonly InfoLatitude model;

        public InfoLatitudeTest()
        {
            this.model = new InfoLatitude(
                "abc",
                this.GetDefinition(),
                this.GetDocbock(),
                this.GetInlineComment()
            );
        }

        [Fact]
        public void TestItSetsLatitude()
        {
            Assert.Equal("abc", this.model.Latitude);
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
                "abc",
                this.model.GetCompileData()
            );
        }
    }
}