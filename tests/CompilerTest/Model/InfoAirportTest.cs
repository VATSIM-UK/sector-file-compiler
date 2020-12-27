using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class InfoAirportTest
    {
        private readonly InfoAirport model;

        public InfoAirportTest()
        {
            this.model = new InfoAirport(
                "EGLL",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsAirportIcao()
        {
            Assert.Equal("EGLL", this.model.AirportIcao);
        }
        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "EGLL",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}