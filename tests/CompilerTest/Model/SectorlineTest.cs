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
            displayRules = SectorLineDisplayRuleFactory.MakeList();
            coordinates = SectorlineCoordinateFactory.MakeList(3);
            model = new Sectorline(
                "Test Sectorline",
                displayRules,
                coordinates,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsName()
        {
            Assert.Equal("Test Sectorline", model.Name);
        }

        [Fact]
        public void TestItSetsDisplayRules()
        {
            Assert.Equal(displayRules, model.DisplayRules);
        }

        [Fact]
        public void TestItSetsCoordinates()
        {
            Assert.Equal(coordinates, model.Coordinates);
        }

        [Fact]
        public void TestItReturnsStartOfLine()
        {
            Assert.Equal(coordinates.First().Coordinate, model.Start());
        }

        [Fact]
        public void TestItReturnsEndOfLine()
        {
            Assert.Equal(coordinates.Last().Coordinate, model.End());
        }

        [Fact]
        public void TestItReturnsCompilableElements()
        {
            IEnumerable<ICompilableElement> expected = new List<ICompilableElement>
                {
                    model
                }
                .Concat(displayRules)
                .Concat(coordinates);

            Assert.Equal(expected, model.GetCompilableElements());
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "SECTORLINE:Test Sectorline",
                model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}
