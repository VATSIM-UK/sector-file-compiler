using Compiler.Injector;
using Compiler.Model;
using Xunit;

namespace CompilerTest.Injector
{
    public class AdjacentAirportsInjectorTest
    {
        private readonly SectorElementCollection sectorElementCollection = new SectorElementCollection();

        public AdjacentAirportsInjectorTest()
        {
            AdjacentAirportsInjector.InjectAdjacentAirportsData(sectorElementCollection);
        }

        [Fact]
        public void TestItInjectsAirport()
        {
            Assert.Single(this.sectorElementCollection.Airports);
            Airport airport = this.sectorElementCollection.Airports[0];
            
            Assert.Equal("Show adjacent departure airports", airport.Name);
            Assert.Equal("000A", airport.Icao);
            Assert.Equal(new Coordinate("S999.00.00.000", "E999.00.00.000"), airport.LatLong);
            Assert.Equal("199.998", airport.Frequency);
            Assert.Equal(new Definition("Defined by compiler", 0), airport.GetDefinition());
            Assert.Equal(new Docblock(), airport.Docblock);
            Assert.Equal(new Comment(""), airport.InlineComment);
        }
        
        [Fact]
        public void TestItInjectsRunway()
        {
            Assert.Single(this.sectorElementCollection.Runways);
            Runway runway = this.sectorElementCollection.Runways[0];
            
            Assert.Equal("000A", runway.AirfieldIcao);
            Assert.Equal("00", runway.FirstIdentifier);
            Assert.Equal(0, runway.FirstHeading);
            Assert.Equal(new Coordinate("S999.00.00.000", "E999.00.00.000"), runway.FirstThreshold);
            Assert.Equal("01", runway.ReverseIdentifier);
            Assert.Equal(0, runway.ReverseHeading);
            Assert.Equal(new Coordinate("S999.00.00.000", "E999.00.00.000"), runway.ReverseThreshold);
            Assert.Equal(new Definition("Defined by compiler", 0), runway.GetDefinition());
            Assert.Equal(new Docblock(), runway.Docblock);
            Assert.Equal(new Comment(""), runway.InlineComment);
        }
    }
}
