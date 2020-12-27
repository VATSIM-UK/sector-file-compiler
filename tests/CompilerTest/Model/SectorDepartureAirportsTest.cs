using Xunit;
using Compiler.Model;
using System.Collections.Generic;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class SectorDepartureAirportsTest
    {
        private readonly SectorDepartureAirports model;
        private readonly List<string> airports = new()
        {
            "EGKK",
            "EGLL"
        };

        public SectorDepartureAirportsTest()
        {
            this.model = new SectorDepartureAirports(
                this.airports,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsAirports()
        {
            Assert.Equal(this.airports, this.model.Airports);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "DEPAPT:EGKK:EGLL",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}