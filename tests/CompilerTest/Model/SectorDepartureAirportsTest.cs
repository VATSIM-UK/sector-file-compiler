using Xunit;
using Compiler.Model;
using System.Collections.Generic;

namespace CompilerTest.Model
{
    public class SectorDepartureAirportsTest
    {
        private readonly SectorDepartureAirports model;
        private readonly List<string> airports = new List<string>
        {
            "EGKK",
            "EGLL"
        };

        public SectorDepartureAirportsTest()
        {
            this.model = new SectorDepartureAirports(
                this.airports,
                "comment"
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
                "DEPAPT:EGKK:EGLL ;comment\r\n",
                this.model.Compile()
            );
        }
    }
}