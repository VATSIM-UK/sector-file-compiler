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

        [Fact]
        public void TestDegreeMinSecToDecimalDegree() {
            Assert.Equal(54.51555556, Coordinate.DegreeMinSecToDecimalDegree("N054.30.56.000"), 0.00000001); // tolerance for float precision
            Assert.Equal(51.26757306, Coordinate.DegreeMinSecToDecimalDegree("N051.16.03.263"), 0.00000001);
            Assert.Equal(-23.85944194, Coordinate.DegreeMinSecToDecimalDegree("S023.51.33.991"), 0.00000001);
            Assert.Equal(0.52033000, Coordinate.DegreeMinSecToDecimalDegree("E000.31.13.188"), 0.00000001);
            Assert.Equal(-3.19839500, Coordinate.DegreeMinSecToDecimalDegree("W003.11.54.222"), 0.00000001);
        }

        [Fact]
        public void TestDecimalDegreeToDegreeMinSec() {
            Assert.Equal("N054.30.56.000", Coordinate.DecimalDegreeToDegreeMinSec(54.51555556, true));
            Assert.Equal("N051.16.03.263", Coordinate.DecimalDegreeToDegreeMinSec(51.26757306, true));
            Assert.Equal("S023.51.33.991", Coordinate.DecimalDegreeToDegreeMinSec(-23.85944194, true));
            Assert.Equal("E000.31.13.188", Coordinate.DecimalDegreeToDegreeMinSec(0.52033000, false));
            Assert.Equal("W003.11.54.222", Coordinate.DecimalDegreeToDegreeMinSec(-3.19839500, false));
        }
    }
}
