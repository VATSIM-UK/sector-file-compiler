using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class InfoMilesPerDegreeLatitudeTest
    {
        private readonly InfoMilesPerDegreeLatitude model;

        public InfoMilesPerDegreeLatitudeTest()
        {
            this.model = new InfoMilesPerDegreeLatitude(
                12,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsMiles()
        {
            Assert.Equal(12, this.model.Miles);
        }
        
        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "12",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}