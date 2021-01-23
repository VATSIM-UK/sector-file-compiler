using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class SectorlineCoordinateTest
    {
        private readonly SectorlineCoordinate model;

        public SectorlineCoordinateTest()
        {
            this.model = new SectorlineCoordinate(
                new Coordinate("abc", "def"),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsCoordinate()
        {
            Assert.Equal(new Coordinate("abc", "def"), this.model.Coordinate);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "COORD:abc:def",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}