using Xunit;
using Compiler.Model;
using System.Collections.Generic;

namespace CompilerTest.Model
{
    public class SectorBorderTest
    {
        private readonly SectorBorder model;
        private List<string> borders = new List<string>
        {
            "ONE",
            "TWO",
            "THREE"
        };

        public SectorBorderTest()
        {
            this.model = new SectorBorder(
                this.borders,
                "comment"
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
                "BORDER:ONE:TWO:THREE ;comment\r\n",
                this.model.Compile()
            );
        }

        [Fact]
        public void TestItCompilesNoData()
        {
            SectorBorder emptyModel = new SectorBorder();
            Assert.Equal(
                "",
                emptyModel.Compile()
            );
        }
    }
}