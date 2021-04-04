using Compiler.Model;
using Xunit;

namespace CompilerTest.Model
{
    public class RadarParametersTest
    {
        [Fact]
        public void ItHasRange()
        {
            RadarParameters parameters = new(1, 2, 3);
            Assert.Equal(1, parameters.Range);
        }
        
        [Fact]
        public void ItHasAltitude()
        {
            RadarParameters parameters = new(1, 2, 3);
            Assert.Equal(2, parameters.Altitude);
        }
        
        [Fact]
        public void ItHasConeSlope()
        {
            RadarParameters parameters = new(1, 2, 3);
            Assert.Equal(3, parameters.ConeSlope);
        }
        
        [Fact]
        public void ItHasFormatsAsStringWithValues()
        {
            RadarParameters parameters = new(1, 2, 3);
            Assert.Equal("1:2:3", parameters.ToString());
        }
        
        [Fact]
        public void ItHasFormatsAsStringWithNoValues()
        {
            RadarParameters parameters = new();
            Assert.Equal("::", parameters.ToString());
        }
    }
}