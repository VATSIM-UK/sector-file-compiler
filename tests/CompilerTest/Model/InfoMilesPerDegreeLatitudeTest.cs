using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class InfoMilesPerDegreeLatitudeTest : AbstractModelTestCase
    {
        private readonly InfoMilesPerDegreeLatitude model;

        public InfoMilesPerDegreeLatitudeTest()
        {
            this.model = new InfoMilesPerDegreeLatitude(
                12,
                this.GetDefinition(),
                this.GetDocbock(),
                this.GetInlineComment()
            );
        }

        [Fact]
        public void TestItSetsCallsign()
        {
            Assert.Equal(12, this.model.Miles);
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