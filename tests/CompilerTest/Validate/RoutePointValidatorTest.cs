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
            sectorElements = new SectorElementCollection();
            sectorElements.Add(FixFactory.Make("testfix"));
            sectorElements.Add(VorFactory.Make("testvor"));
            sectorElements.Add(NdbFactory.Make("testndb"));
            sectorElements.Add(AirportFactory.Make("testairport"));
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
            Assert.Equal(expected, RoutePointValidator.IsValidVor(identifier, sectorElements));
        }

        [Theory]
        [InlineData("testndb", true)]
        [InlineData("nottestndb", false)]
        public void TestItChecksNdbs(string identifier, bool expected)
        {
            Assert.Equal(expected, RoutePointValidator.IsValidNdb(identifier, sectorElements));
        }

        [Theory]
        [InlineData("testfix", true)]
        [InlineData("nottestfix", false)]
        public void TestItChecksFixes(string identifier, bool expected)
        {
            Assert.Equal(expected, RoutePointValidator.IsValidFix(identifier, sectorElements));
        }

        [Theory]
        [InlineData("testairport", true)]
        [InlineData("nottestairport", false)]
        public void TestItChecksAirpots(string identifier, bool expected)
        {
            Assert.Equal(expected, RoutePointValidator.IsValidAirport(identifier, sectorElements));
        }

        [Fact]
        public void TestItValidatesFalseIfAllStepsFail()
        {
            Point point = new("what");
            Assert.False(RoutePointValidator.ValidatePoint(point, sectorElements));
        }

        [Fact]
        public void TestItValidatesTrueIfValidCoordinate()
        {
            Point point = new(new Coordinate("abc", "def"));
            Assert.True(RoutePointValidator.ValidatePoint(point, sectorElements));
        }

        [Fact]
        public void TestItValidatesTrueIfValidVor()
        {
            Point point = new("testvor");
            Assert.True(RoutePointValidator.ValidatePoint(point, sectorElements));
        }

        [Fact]
        public void TestItValidatesTrueIfValidNdb()
        {
            Point point = new("testndb");
            Assert.True(RoutePointValidator.ValidatePoint(point, sectorElements));
        }

        [Fact]
        public void TestItValidatesTrueIfValidFix()
        {
            Point point = new("testfix");
            Assert.True(RoutePointValidator.ValidatePoint(point, sectorElements));
        }

        [Fact]
        public void TestItValidatesTrueIfValidAirport()
        {
            Point point = new("testairport");
            Assert.True(RoutePointValidator.ValidatePoint(point, sectorElements));
        }

        [Fact]
        public void TestItValidatesEseFalseIfAllStepsFail()
        {
            Assert.False(RoutePointValidator.ValidateEseSidStarPoint("what", sectorElements));
        }

        [Fact]
        public void TestItValidatesEseTrueIfValidVor()
        {
            Assert.True(RoutePointValidator.ValidateEseSidStarPoint("testvor", sectorElements));
        }

        [Fact]
        public void TestItValidatesEseTrueIfValidNdb()
        {
            Assert.True(RoutePointValidator.ValidateEseSidStarPoint("testndb", sectorElements));
        }

        [Fact]
        public void TestItValidatesEseTrueIfValidFix()
        {
            Assert.True(RoutePointValidator.ValidateEseSidStarPoint("testfix", sectorElements));
        }

        [Fact]
        public void TestItValidatesEseTrueIfValidAirport()
        {
            Assert.True(RoutePointValidator.ValidateEseSidStarPoint("testairport", sectorElements));
        }
    }
}
