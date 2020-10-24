using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class InfoScaleTest : AbstractModelTestCase
    {
        private readonly InfoScale model;

        public InfoScaleTest()
        {
            this.model = new InfoScale(
                12,
                this.GetDefinition(),
                this.GetDocbock(),
                this.GetInlineComment()
            );
        }

        [Fact]
        public void TestItSetsScale()
        {
            Assert.Equal(12, this.model.Scale);
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
                "12",
                this.model.GetCompileData()
            );
        }
    }
}