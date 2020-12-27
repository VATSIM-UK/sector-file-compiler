using Xunit;
using System.Collections.Generic;
using System.Linq;
using Compiler.Model;
using CompilerTest.Bogus;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class RegionTest
    {
        private readonly Region region;

        private readonly List<RegionPoint> points = RegionPointFactory.MakeList(3);

        public RegionTest()
        {
            this.region = new Region(
                "Region1",
                this.points,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsName()
        {
            Assert.Equal("Region1", this.region.Name);
        }

        [Fact]
        public void TestItSetsPoints()
        {
            Assert.Equal(this.points, this.region.Points);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "REGIONNAME Region1",
                this.region.GetCompileData(new SectorElementCollection())
            );
        }

        [Fact]
        public void TestItReturnsCompilableElements()
        {
            IEnumerable<ICompilableElement> expected = new List<ICompilableElement>()
            {
                this.region
            }.Concat(this.points);
            Assert.Equal(expected, this.region.GetCompilableElements());
        }
    }
}
