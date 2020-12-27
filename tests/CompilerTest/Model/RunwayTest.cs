using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class RunwayTest
    {
        private readonly Runway runway;
        private readonly Airport airport;
        private readonly SectorElementCollection sectorElements = new SectorElementCollection();
        
        public RunwayTest()
        {
            this.airport = AirportFactory.Make();
            this.sectorElements.Add(this.airport);
            this.runway = new Runway(
                this.airport.Icao,
                "09",
                90,
                new Coordinate("abc", "def"),
                "27",
                270,
                new Coordinate("ghi", "jkl"),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsFirstIdentifier()
        {
            Assert.Equal("09", this.runway.FirstIdentifier);
        }

        [Fact]
        public void TestItSetsFirstHeading()
        {
            Assert.Equal(90, this.runway.FirstHeading);
        }

        [Fact]
        public void TestItSetsFirstThreshold()
        {
            Assert.Equal(new Coordinate("abc", "def"), this.runway.FirstThreshold);
        }

        [Fact]
        public void TestItSetsReverseIdentifier()
        {
            Assert.Equal("27", this.runway.ReverseIdentifier);
        }

        [Fact]
        public void TestItSetsReserveHeading()
        {
            Assert.Equal(270, this.runway.ReverseHeading);
        }

        [Fact]
        public void TestItSetsReverseThreshold()
        {
            Assert.Equal(new Coordinate("ghi", "jkl"), this.runway.ReverseThreshold);
        }

        [Fact]
        public void TestItSetsAirfieldIcao()
        {
            Assert.Equal(this.airport.Icao, this.runway.AirfieldIcao);
        }


        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                $"09 27 090 270 abc def ghi jkl {this.airport.Icao} - {this.airport.Name}",
                this.runway.GetCompileData(this.sectorElements)
            );
        }
    }
}
