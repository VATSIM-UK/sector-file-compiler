using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class InfoLatitudeTest
    {
        private readonly InfoLatitude model;

        public InfoLatitudeTest()
        {
            this.model = new InfoLatitude(
                "abc",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsLatitude()
        {
            Assert.Equal("abc", this.model.Latitude);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "abc",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}