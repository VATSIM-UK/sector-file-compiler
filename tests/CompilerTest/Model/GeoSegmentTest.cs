using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class GeoSegmentTest
    {
        private GeoSegment segment;

        public GeoSegmentTest()
        {
            this.segment = new GeoSegment(
                new Point(new Coordinate("abc", "def")),
                new Point(new Coordinate("ghi", "jkl")),
                "red",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsFirstCoordinate()
        {
            Assert.Equal(new Point(new Coordinate("abc", "def")), this.segment.FirstPoint);
        }

        [Fact]
        public void TestItSetsSecondCoordinate()
        {
            Assert.Equal(new Point(new Coordinate("ghi", "jkl")), this.segment.SecondPoint);
        }

        [Fact]
        public void TestItSetsColour()
        {
            Assert.Equal("red", this.segment.Colour);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("abc def ghi jkl red", this.segment.GetCompileData(new SectorElementCollection()));
        }

        [Fact]
        public void TestItCompilesWithNoColour()
        {
            GeoSegment segment = new(
                new Point(new Coordinate("abc", "def")),
                new Point(new Coordinate("ghi", "jkl")),
                null,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            Assert.Equal("abc def ghi jkl", segment.GetCompileData(new SectorElementCollection()));
        }
    }
}
