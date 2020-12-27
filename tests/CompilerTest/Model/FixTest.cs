using Compiler.Model;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Model
{
    public class FixTest
    {
        private readonly Fix fix;

        public FixTest()
        {
            this.fix = new Fix(
                "ADMAG",
                new Coordinate("abc", "def"),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsIdentifier()
        {
            Assert.Equal("ADMAG", this.fix.Identifier);
        }

        [Fact]
        public void TestItSetsCoordinate() 
        {
            Assert.Equal(new Coordinate("abc", "def"), this.fix.Coordinate);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("ADMAG abc def", this.fix.GetCompileData(new SectorElementCollection()));
        }
    }
}
