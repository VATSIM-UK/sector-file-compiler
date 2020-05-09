using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class InfoTest
    {
        private readonly Info model;

        public InfoTest()
        {
            this.model = new Info(
                "Super Cool Sector",
                "LON_CTR",
                "EGLL",
                new Coordinate("123", "456"),
                60,
                40.24,
                2.1,
                1
            );
        }

        [Fact]
        public void TestItSetsName()
        {
            Assert.Equal("Super Cool Sector", this.model.Name);
        }

        [Fact]
        public void TestItSetsCallsign()
        {
            Assert.Equal("LON_CTR", this.model.Callsign);
        }

        [Fact]
        public void TestItSetsAirport()
        {
            Assert.Equal("EGLL", this.model.Airport);
        }

        [Fact]
        public void TestItSetsCoordinate()
        {
            Assert.Equal(new Coordinate("123", "456"), this.model.Coordinate);
        }

        [Fact]
        public void TestItSetsMilesPerDegreeLatitude()
        {
            Assert.Equal(60, this.model.MilesPerDegreeLatitude);
        }

        [Fact]
        public void TestItSetsMilesPerDegreeLongitude()
        {
            Assert.Equal(40.24, this.model.MilesPerDegreeLongitude);
        }

        [Fact]
        public void TestItSetsMagneticVariation()
        {
            Assert.Equal(2.1, this.model.MagneticVariation);
        }

        [Fact]
        public void TestItSetsScale()
        {
            Assert.Equal(1, this.model.Scale);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "Super Cool Sector\r\nLON_CTR\r\nEGLL\r\n123\r\n456\r\n60\r\n40.24\r\n2.1\r\n1\r\n",
                this.model.Compile()
            );
        }
    }
}