using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class SectorlineDisplayRuleTest
    {
        private readonly SectorlineDisplayRule model;

        public SectorlineDisplayRuleTest()
        {
            this.model = new SectorlineDisplayRule(
                "Deancross",
                "Deancross",
                "East",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsControlledSector()
        {
            Assert.Equal("Deancross", this.model.ControlledSector);
        }

        [Fact]
        public void TestItSetsFirstCompareSector()
        {
            Assert.Equal("Deancross", this.model.CompareSectorFirst);
        }

        [Fact]
        public void TestItSetsSecondCompareSector()
        {
            Assert.Equal("East", this.model.CompareSectorSecond);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "DISPLAY:Deancross:Deancross:East",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}