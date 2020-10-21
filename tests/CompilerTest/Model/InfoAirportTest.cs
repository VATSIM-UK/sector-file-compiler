using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class InfoAirportTest : AbstractModelTestCase
    {
        private readonly InfoAirport model;

        public InfoAirportTest()
        {
            this.model = new InfoAirport(
                "EGLL",
                this.GetDefinition(),
                this.GetDocbock(),
                this.GetInlineComment()
            );
        }

        [Fact]
        public void TestItSetsAirportIcao()
        {
            Assert.Equal("EGLL", this.model.AirportIcao);
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
                "EGLL",
                this.model.GetCompileData()
            );
        }
    }
}