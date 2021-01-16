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
            this.regionPoint = new RegionPoint(
                new Point("TEST"),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );

            this.regionPointWithColour = new RegionPoint(
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
            Assert.Equal(new Point("TEST"), this.regionPoint.Point);
        }

        [Fact]
        public void TestItSetsColourAsNullIfNone()
        {
            Assert.Null(this.regionPoint.Colour);
        }

        [Fact]
        public void TestItSetsColourIfProvided()
        {
            Assert.Equal("red", this.regionPointWithColour.Colour);
        }

        [Fact]
        public void TestItCompilesWithNoColour()
        {
            Assert.Equal("TEST TEST", this.regionPoint.GetCompileData(new SectorElementCollection()));
        }

        [Fact]
        public void TestItCompilesWithColour()
        {
            Assert.Equal("red TEST TEST", this.regionPointWithColour.GetCompileData(new SectorElementCollection()));
        }
    }
}
