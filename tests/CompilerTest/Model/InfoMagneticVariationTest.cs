using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class InfoMagneticVariationTest : AbstractModelTestCase
    {
        private readonly InfoMagneticVariation model;

        public InfoMagneticVariationTest()
        {
            this.model = new InfoMagneticVariation(
                12.154,
                this.GetDefinition(),
                this.GetDocbock(),
                this.GetInlineComment()
            );
        }

        [Fact]
        public void TestItSetsVariation()
        {
            Assert.Equal(12.154, this.model.Variation);
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
                "12.2",
                this.model.GetCompileData()
            );
        }
    }
}