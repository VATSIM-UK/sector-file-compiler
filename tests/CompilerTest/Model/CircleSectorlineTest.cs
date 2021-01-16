using Xunit;
using Compiler.Model;
using System.Collections.Generic;
using System.Linq;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class CircleSectorlineTest
    {
        private readonly CircleSectorline model1;
        private readonly CircleSectorline model2;
        private readonly List<SectorlineDisplayRule> displayRules = new()
        {
            SectorLineDisplayRuleFactory.Make(),
            SectorLineDisplayRuleFactory.Make(),
        };

        public CircleSectorlineTest()
        {
            this.model1 = new CircleSectorline(
                "Test Sectorline",
                "EGGD",
                5.5,
                this.displayRules,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );

            this.model2 = new CircleSectorline(
                "Test Sectorline",
                new Coordinate("abc", "def"),
                5.5,
                this.displayRules,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestPointVersionSetsName()
        {
            Assert.Equal("Test Sectorline", this.model1.Name);
        }

        [Fact]
        public void TestPointVersionSetsCentrePoint()
        {
            Assert.Equal("EGGD", this.model1.CentrePoint);
        }

        [Fact]
        public void TestPointVersionSetsRadius()
        {
            Assert.Equal(5.5, this.model1.Radius);
        }

        [Fact]
        public void TestPointVersionSetsDisplayRules()
        {
            Assert.Equal(this.displayRules, this.model1.DisplayRules);
        }

        [Fact]
        public void TestCoordinateVersionSetsName()
        {
            Assert.Equal("Test Sectorline", this.model2.Name);
        }

        [Fact]
        public void TestCoordinateVersionSetsCentreCoordinate()
        {
            Assert.Equal(new Coordinate("abc", "def"), this.model2.CentreCoordinate);
        }

        [Fact]
        public void TestCoordinateVersionSetsRadius()
        {
            Assert.Equal(5.5, this.model2.Radius);
        }

        [Fact]
        public void TestCoordinateVersionSetsDisplayRules()
        {
            Assert.Equal(this.displayRules, this.model2.DisplayRules);
        }

        [Fact]
        public void TestItReturnsCompilableElements()
        {
            IEnumerable<ICompilableElement> expected = new List<ICompilableElement>()
            {
                this.model1,
            }.Concat(this.displayRules);
            Assert.Equal(expected, this.model1.GetCompilableElements());
        }

        [Fact]
        public void TestPointVersionCompiles()
        {
            Assert.Equal(
                "CIRCLE_SECTORLINE:Test Sectorline:EGGD:5.5",
                this.model1.GetCompileData(new SectorElementCollection())
            );
        }

        [Fact]
        public void TestCoordinateVersionCompiles()
        {
            Assert.Equal(
                "CIRCLE_SECTORLINE:Test Sectorline:abc:def:5.5",
                this.model2.GetCompileData(new SectorElementCollection())
            );
        }
    }
}