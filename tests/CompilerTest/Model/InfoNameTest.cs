using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class InfoNameTest : AbstractModelTestCase
    {
        private readonly InfoName model;

        public InfoNameTest()
        {
            this.model = new InfoName(
                "Super Cool Sector",
                this.GetDefinition(),
                this.GetDocbock(),
                this.GetInlineComment()
            );
        }

        [Fact]
        public void TestItSetsName()
        {
            Assert.Equal("Super Cool Sector", this.model.Name);
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
                "Super Cool Sector",
                this.model.GetCompileData()
            );
        }
    }
}