using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class ColourTest
    {
        private readonly Colour colour;

        public ColourTest()
        {
            this.colour = new Colour("colour1", 123);
        }

        [Fact]
        public void TestItSetsName()
        {
            Assert.Equal("colour1", this.colour.Name);
        }

        [Fact]
        public void TestItSetsValue()
        {
            Assert.Equal(123, this.colour.Value);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("#define colour1 123\r\n", this.colour.Compile());
        }
    }
}
