using Xunit;
using Compiler.Model;
using System.Collections.Generic;

namespace CompilerTest.Model
{
    public class SectorArrivalAirportsTest
    {
        private readonly SectorArrivalAirports model;
        private readonly List<string> airports = new List<string>
        {
            "EGKK",
            "EGLL"
        };

        public SectorArrivalAirportsTest()
        {
            this.model = new SectorArrivalAirports(
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
                "ARRAPT:EGKK:EGLL ;comment\r\n",
                this.model.Compile()
            );
        }
    }
}