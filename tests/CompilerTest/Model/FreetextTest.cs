using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class FreetextTest
    {
        private readonly Freetext model;

        public FreetextTest()
        {
            this.model = new Freetext(
                "Freetext",
                "Some text",
                new Coordinate("abc", "def"),
                "comment"
            );
        }

        [Fact]
        public void TestItSetsTitle()
        {
            Assert.Equal("Freetext", this.model.Title);
        }

        [Fact]
        public void TestItSetsText()
        {
            Assert.Equal("Some text", this.model.Text);
        }

        [Fact]
        public void TestItSetsCoordinate()
        {
            Assert.Equal(new Coordinate("abc", "def"), this.model.Coordinate);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("abc:def:Freetext:Some text ;comment\r\n", this.model.Compile());
        }
    }
}
