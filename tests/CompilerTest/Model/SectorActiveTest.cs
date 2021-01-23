using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class SectorActiveTest
    {
        private readonly SectorActive model;

        public SectorActiveTest()
        {
            this.model = new SectorActive(
                "EGLL",
                "09R",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsAirfield()
        {
            Assert.Equal("EGLL", this.model.Airfield);
        }

        [Fact]
        public void TestItSetsRunway()
        {
            Assert.Equal("09R", this.model.Runway);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "ACTIVE:EGLL:09R",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}