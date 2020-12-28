using Xunit;
using Compiler.Model;
using System.Collections.Generic;
using System.Linq;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class SectorlineTest
    {
        private readonly Sectorline model;
        private readonly List<SectorlineDisplayRule> displayRules;
        private readonly List<SectorlineCoordinate> coordinates;

        public SectorlineTest()
        {
            this.displayRules = SectorLineDisplayRuleFactory.MakeList();
            this.coordinates = SectorlineCoordinateFactory.MakeList();
            this.model = new Sectorline(
                "Test Sectorline",
                this.displayRules,
                this.coordinates,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsName()
        {
            Assert.Equal("Test Sectorline", this.model.Name);
        }

        [Fact]
        public void TestItSetsDisplayRules()
        {
            Assert.Equal(this.displayRules, this.model.DisplayRules);
        }

        [Fact]
        public void TestItSetsCoordinates()
        {
            Assert.Equal(this.coordinates, this.model.Coordinates);
        }

        [Fact]
        public void TestItReturnsCompilableElements()
        {
            IEnumerable<ICompilableElement> expected = new List<ICompilableElement>
            {
                this.model
            }
                .Concat(this.displayRules)
                .Concat(this.coordinates);
            
            Assert.Equal(expected, this.model.GetCompilableElements());
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "SECTORLINE:Test Sectorline",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}