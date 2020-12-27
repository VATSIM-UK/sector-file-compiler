using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class InfoNameTest
    {
        private readonly InfoName model;

        public InfoNameTest()
        {
            this.model = new InfoName(
                "Super Cool Sector",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsName()
        {
            Assert.Equal("Super Cool Sector", this.model.Name);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "Super Cool Sector",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}