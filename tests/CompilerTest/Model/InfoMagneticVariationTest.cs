using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class InfoMagneticVariationTest
    {
        private readonly InfoMagneticVariation model;

        public InfoMagneticVariationTest()
        {
            this.model = new InfoMagneticVariation(
                12.154,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsVariation()
        {
            Assert.Equal(12.154, this.model.Variation);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "12.2",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}