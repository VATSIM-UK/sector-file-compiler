using Compiler.Model;
using Xunit;

namespace CompilerTest.Model
{
    public class CoordinateTest
    {
        private readonly Coordinate coordinate;

        public CoordinateTest()
        {
            this.coordinate = new Coordinate("abc", "def");
        }

        [Fact]
        public void TestItSetsLatitude()
        {
            Assert.Equal("abc", coordinate.latitude);
        }

        [Fact]
        public void TestItSetsLongitude()
        {
            Assert.Equal("def", coordinate.longitude);
        }

        [Fact]
        public void TestItRepresentsAsString()
        {
            Assert.Equal("abc def", coordinate.ToString());
        }

        [Theory]
        [InlineData(54.51555556, "N054.30.56.000")]
        [InlineData(51.26757306, "N051.16.03.263")]
        [InlineData(-23.85944194, "S023.51.33.991")]
        [InlineData(0.52033000, "E000.31.13.188")]
        [InlineData(-3.19839500, "W003.11.54.222")]
        public void TestDegreeMinSecToDecimalDegree(double expected, string coordinateString) {
            Assert.Equal(expected, Coordinate.DegreeMinSecToDecimalDegree(coordinateString), 0.00000001); // tolerance for float precision
        }

        [Theory]
        [InlineData(54.51555556, "N054.30.56.000")]
        [InlineData(51.26757306, "N051.16.03.263")]
        [InlineData(-23.85944194, "S023.51.33.991")]
        [InlineData(0.52033000, "E000.31.13.188")]
        [InlineData(-3.19839500, "W003.11.54.222")]
        public void TestDecimalDegreeToDegreeMinSec(double coordinate, string expected) {
            Assert.Equal(expected, Coordinate.DecimalDegreeToDegreeMinSec(coordinate, true));
        }
    }
}
