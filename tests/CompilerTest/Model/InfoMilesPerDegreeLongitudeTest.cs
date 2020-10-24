using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class InfoMilesPerDegreLongitudeTest : AbstractModelTestCase
    {
        private readonly InfoMilesPerDegreeLongitude model;

        public InfoMilesPerDegreLongitudeTest()
        {
            this.model = new InfoMilesPerDegreeLongitude(
                12.1234,
                this.GetDefinition(),
                this.GetDocbock(),
                this.GetInlineComment()
            );
        }

        [Fact]
        public void TestItSetsCallsign()
        {
            Assert.Equal(12.1254, this.model.Miles);
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
                "12.13",
                this.model.GetCompileData()
            );
        }
    }
}