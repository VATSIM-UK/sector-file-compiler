using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class AirportTest
    {
        private readonly Airport airport;

        public AirportTest()
        {
            this.airport = new Airport(
                "Testville",
                "EGTT",
                new Coordinate("abc", "def"),
                "123.456",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsName()
        {
            Assert.Equal("Testville", this.airport.Name);
        }

        [Fact]
        public void TestItSetsIcao()
        {
            Assert.Equal("EGTT", this.airport.Icao);
        }

        [Fact]
        public void TestItSetsLatLong()
        {
            Assert.Equal("abc def", this.airport.LatLong.ToString());
        }

        [Fact]
        public void TestItSetsFrequency()
        {
            Assert.Equal("123.456", this.airport.Frequency);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "EGTT 123.456 abc def E",
                this.airport.GetCompileData(new SectorElementCollection())
            );
        }
    }
}
