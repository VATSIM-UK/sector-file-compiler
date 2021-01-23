using Xunit;
using Compiler.Model;
using System.Collections.Generic;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class SectorArrivalAirportsTest
    {
        private readonly SectorArrivalAirports model;
        private readonly List<string> airports = new()
        {
            "EGKK",
            "EGLL"
        };

        public SectorArrivalAirportsTest()
        {
            this.model = new SectorArrivalAirports(
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
                "ARRAPT:EGKK:EGLL",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}