using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class InfoScaleTest
    {
        private readonly InfoScale model;

        public InfoScaleTest()
        {
            this.model = new InfoScale(
                12,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsScale()
        {
            Assert.Equal(12, this.model.Scale);
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