using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class InfoMilesPerDegreeLongitudeTest
    {
        private readonly InfoMilesPerDegreeLongitude model;

        public InfoMilesPerDegreeLongitudeTest()
        {
            this.model = new InfoMilesPerDegreeLongitude(
                12.1254,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsMiles()
        {
            Assert.Equal(12.1254, this.model.Miles);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "12.13",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}