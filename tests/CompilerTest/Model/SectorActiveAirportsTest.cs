using Xunit;
using Compiler.Model;
using System.Collections.Generic;

namespace CompilerTest.Model
{
    public class SectorActiveAirportsTest
    {
        private readonly SectorActiveAirports model;
        private readonly List<string> airports = new List<string>
        {
            "EGKK",
            "EGLL"
        };

        public SectorActiveAirportsTest()
        {
            this.model = new SectorActiveAirports(
                true,
                this.airports,
                "comment"
            );
        }

        [Fact]
        public void TestItSetsDepartureAirport()
        {
            Assert.True(this.model.DepartureAirport);
        }

        [Fact]
        public void TestItSetsAirports()
        {
            Assert.Equal(this.airports, this.model.Airports);
        }

        [Fact]
        public void TestItCompilesDepartureAirports()
        {
            Assert.Equal(
                "DEPAPT:EGKK:EGLL ;comment\r\n",
                this.model.Compile()
            );
        }

        [Fact]
        public void TestItCompilesArrivalAirports()
        {
            SectorActiveAirports model2 = new SectorActiveAirports(
                false,
                this.airports,
                "comment"
            );

            Assert.Equal(
                "ARRAPT:EGKK:EGLL ;comment\r\n",
                model2.Compile()
            );
        }
    }
}