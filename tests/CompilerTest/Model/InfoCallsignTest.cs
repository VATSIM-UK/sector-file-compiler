using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class InfoCallsignTest : AbstractModelTestCase
    {
        private readonly InfoCallsign model;

        public InfoCallsignTest()
        {
            this.model = new InfoCallsign(
                "LON_CTR",
                this.GetDefinition(),
                this.GetDocbock(),
                this.GetInlineComment()
            );
        }

        [Fact]
        public void TestItSetsCallsign()
        {
            Assert.Equal("LON_CTR", this.model.Callsign);
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
                "LON_CTR",
                this.model.GetCompileData()
            );
        }
    }
}