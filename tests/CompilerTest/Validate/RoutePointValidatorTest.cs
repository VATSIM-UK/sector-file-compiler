using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class RoutePointValidatorTest
    {
        private readonly SectorElementCollection sectorElements;

        public RoutePointValidatorTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.sectorElements.Add(FixFactory.Make("testfix"));
            this.sectorElements.Add(VorFactory.Make("testvor"));
            this.sectorElements.Add(NdbFactory.Make("testndb"));
            this.sectorElements.Add(AirportFactory.Make("testairport"));
        }

        [Fact]
        public void TestItReturnsTrueOnValidCoordinate()
        {
            Point point = new(new Coordinate("abc", "def"));
            Assert.True(RoutePointValidator.IsValidCoordinate(point));
        }

        [Fact]
        public void TestItReturnsFalseOnInvalidCoordinate()
        {
            Point point = new("abc");
            Assert.False(RoutePointValidator.IsValidCoordinate(point));
        }

        [Theory]
        [InlineData("testvor", true)]
        [InlineData("nottestvor", false)]
        public void TestItChecksVors(string identifier, bool expected)
        {
            Assert.Equal(expected, RoutePointValidator.IsValidVor(identifier, this.sectorElements));
        }

        [Theory]
        [InlineData("testndb", true)]
        [InlineData("nottestndb", false)]
        public void TestItChecksNdbs(string identifier, bool expected)
        {
            Assert.Equal(expected, RoutePointValidator.IsValidNdb(identifier, this.sectorElements));
        }

        [Theory]
        [InlineData("testfix", true)]
        [InlineData("nottestfix", false)]
        public void TestItChecksFixes(string identifier, bool expected)
        {
            Assert.Equal(expected, RoutePointValidator.IsValidFix(identifier, this.sectorElements));
        }

        [Theory]
        [InlineData("testairport", true)]
        [InlineData("nottestairport", false)]
        public void TestItChecksAirpots(string identifier, bool expected)
        {
            Assert.Equal(expected, RoutePointValidator.IsValidAirport(identifier, this.sectorElements));
        }

        [Fact]
        public void TestItValidatesFalseIfAllStepsFail()
        {
            Point point = new("what");
            Assert.False(RoutePointValidator.ValidatePoint(point, this.sectorElements));
        }

        [Fact]
        public void TestItValidatesTrueIfValidCoordinate()
        {
            Point point = new(new Coordinate("abc", "def"));
            Assert.True(RoutePointValidator.ValidatePoint(point, this.sectorElements));
        }

        [Fact]
        public void TestItValidatesTrueIfValidVor()
        {
            Point point = new("testvor");
            Assert.True(RoutePointValidator.ValidatePoint(point, this.sectorElements));
        }

        [Fact]
        public void TestItValidatesTrueIfValidNdb()
        {
            Point point = new("testndb");
            Assert.True(RoutePointValidator.ValidatePoint(point, this.sectorElements));
        }

        [Fact]
        public void TestItValidatesTrueIfValidFix()
        {
            Point point = new("testfix");
            Assert.True(RoutePointValidator.ValidatePoint(point, this.sectorElements));
        }

        [Fact]
        public void TestItValidatesTrueIfValidAirport()
        {
            Point point = new("testairport");
            Assert.True(RoutePointValidator.ValidatePoint(point, this.sectorElements));
        }

        [Fact]
        public void TestItValidatesEseFalseIfAllStepsFail()
        {
            Assert.False(RoutePointValidator.ValidateEseSidStarPoint("what", this.sectorElements));
        }

        [Fact]
        public void TestItValidatesEseTrueIfValidVor()
        {
            Assert.True(RoutePointValidator.ValidateEseSidStarPoint("testvor", this.sectorElements));
        }

        [Fact]
        public void TestItValidatesEseTrueIfValidNdb()
        {
            Assert.True(RoutePointValidator.ValidateEseSidStarPoint("testndb", this.sectorElements));
        }

        [Fact]
        public void TestItValidatesEseTrueIfValidFix()
        {
            Assert.True(RoutePointValidator.ValidateEseSidStarPoint("testfix", this.sectorElements));
        }

        [Fact]
        public void TestItValidatesEseTrueIfValidAirport()
        {
            Assert.True(RoutePointValidator.ValidateEseSidStarPoint("testairport", this.sectorElements));
        }
    }
}
