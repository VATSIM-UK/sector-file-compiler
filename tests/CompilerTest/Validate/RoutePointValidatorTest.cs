using Xunit;
using Compiler.Model;
using Compiler.Validate;

namespace CompilerTest.Validate
{
    public class RoutePointValidatorTest
    {
        private readonly SectorElementCollection sectorElements;

        public RoutePointValidatorTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.sectorElements.Add(new Fix("testfix", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Vor("testvor", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Ndb("testndb", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Airport("testairport", "testairport", new Coordinate("abc", "def"), "123.456", "test"));
        }

        [Fact]
        public void TestItReturnsTrueOnValidCoordinate()
        {
            Point point = new Point(new Coordinate("abc", "def"));
            Assert.True(RoutePointValidator.IsValidCoordinate(point));
        }

        [Fact]
        public void TestItReturnsFalseOnInvalidCoordinate()
        {
            Point point = new Point("abc");
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
            Point point = new Point("what");
            Assert.False(RoutePointValidator.ValidatePoint(point, this.sectorElements));
        }

        [Fact]
        public void TestItValidatesTrueIfValidCoordinate()
        {
            Point point = new Point(new Coordinate("abc", "def"));
            Assert.True(RoutePointValidator.ValidatePoint(point, this.sectorElements));
        }

        [Fact]
        public void TestItValidatesTrueIfValidVor()
        {
            Point point = new Point("testvor");
            Assert.True(RoutePointValidator.ValidatePoint(point, this.sectorElements));
        }

        [Fact]
        public void TestItValidatesTrueIfValidNdb()
        {
            Point point = new Point("testndb");
            Assert.True(RoutePointValidator.ValidatePoint(point, this.sectorElements));
        }

        [Fact]
        public void TestItValidatesTrueIfValidFix()
        {
            Point point = new Point("testfix");
            Assert.True(RoutePointValidator.ValidatePoint(point, this.sectorElements));
        }

        [Fact]
        public void TestItValidatesTrueIfValidAirport()
        {
            Point point = new Point("testairport");
            Assert.True(RoutePointValidator.ValidatePoint(point, this.sectorElements));
        }
    }
}
