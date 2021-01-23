using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class InfoLongitudeTest
    {
        private readonly InfoLongitude model;

        public InfoLongitudeTest()
        {
            this.model = new InfoLongitude(
                "def",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsLongitude()
        {
            Assert.Equal("def", this.model.Longitude);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "def",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}