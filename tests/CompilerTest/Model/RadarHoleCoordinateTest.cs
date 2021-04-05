using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class RadarHoleCoordinateTest
    {
        private readonly RadarHoleCoordinate coordinate;

        public RadarHoleCoordinateTest()
        {
            coordinate = new RadarHoleCoordinate(
                new Coordinate("abc", "def"),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsCoordinate()
        {
            Assert.Equal(new Coordinate("abc", "def"), coordinate.Coordinate);
        }
        
        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("COORD:abc:def", coordinate.GetCompileData(new SectorElementCollection()));
        }
    }
}
