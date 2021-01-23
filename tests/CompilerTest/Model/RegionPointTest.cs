using Compiler.Model;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Model
{
    public class RegionPointTest
    {
        private readonly RegionPoint regionPoint;
        private readonly RegionPoint regionPointWithColour;

        public RegionPointTest()
        {
            regionPoint = new RegionPoint(
                new Point("TEST"),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );

            regionPointWithColour = new RegionPoint(
                new Point("TEST"),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make(),
                "red"
            );
        }

        [Fact]
        public void TestItSetsPoint()
        {
            Assert.Equal(new Point("TEST"), regionPoint.Point);
        }

        [Fact]
        public void TestItSetsColourAsNullIfNone()
        {
            Assert.Null(regionPoint.Colour);
        }

        [Fact]
        public void TestItSetsColourIfProvided()
        {
            Assert.Equal("red", regionPointWithColour.Colour);
        }

        [Fact]
        public void TestItCompilesWithNoColour()
        {
            Assert.Equal(" TEST TEST", regionPoint.GetCompileData(new SectorElementCollection()));
        }

        [Fact]
        public void TestItCompilesWithColour()
        {
            Assert.Equal("red TEST TEST", regionPointWithColour.GetCompileData(new SectorElementCollection()));
        }
    }
}
