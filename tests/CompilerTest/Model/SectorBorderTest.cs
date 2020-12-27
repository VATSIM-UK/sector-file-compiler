using Xunit;
using Compiler.Model;
using System.Collections.Generic;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class SectorBorderTest
    {
        private readonly SectorBorder model;
        private List<string> borders = new()
        {
            "ONE",
            "TWO",
            "THREE"
        };

        public SectorBorderTest()
        {
            this.model = new SectorBorder(
                this.borders,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsBorderName()
        {
            Assert.Equal(this.borders, this.model.BorderLines);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "BORDER:ONE:TWO:THREE",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}