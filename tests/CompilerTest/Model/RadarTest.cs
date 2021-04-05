using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class RadarTest
    {
        private readonly Radar model;

        public RadarTest()
        {
            model = new Radar(
                "TESTRADAR",
                new Coordinate("abc", "def"),
                new RadarParameters(1, 2, 3),
                new RadarParameters(4, 5, 6),
                new RadarParameters(7, 8, 9),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsName()
        {
            Assert.Equal("TESTRADAR", model.Name);
        }

        [Fact]
        public void TestItSetsCoordinate()
        {
            Assert.Equal(new Coordinate("abc", "def"), model.Coordinate);
        }
        
        [Fact]
        public void TestItSetsPrimaryParameters()
        {
            Assert.Equal(new RadarParameters(1, 2, 3), model.PrimaryRadarParameters);
        }
        
        [Fact]
        public void TestItSetsSModeParameters()
        {
            Assert.Equal(new RadarParameters(4, 5, 6), model.SModeRadarParameters);
        }
        
        [Fact]
        public void TestItSetsCModeParameters()
        {
            Assert.Equal(new RadarParameters(7, 8, 9), model.CModeRadarParameters);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("RADAR2:TESTRADAR:abc:def:1:2:3:4:5:6:7:8:9", model.GetCompileData(new SectorElementCollection()));
        }
    }
}
