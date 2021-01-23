using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class ColourTest
    {
        private readonly Colour colour;

        public ColourTest()
        {
            this.colour = new Colour(
                "colour1",
                123,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
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
            Assert.Equal("#define colour1 123", this.colour.GetCompileData(new SectorElementCollection()));
        }
    }
}
