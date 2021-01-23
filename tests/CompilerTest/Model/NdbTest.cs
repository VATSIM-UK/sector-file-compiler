using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class NdbTest
    {
        private readonly Ndb model;

        public NdbTest()
        {
            this.model = new Ndb(
                "BRI",
                "123.456",
                new Coordinate("abc", "def"),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsIdentifier()
        {
            Assert.Equal("BRI", this.model.Identifier);
        }

        [Fact]
        public void TestItSetsFrequency()
        {
            Assert.Equal("123.456", this.model.Frequency);
        }

        [Fact]
        public void TestItSetsCoordinate()
        {
            Assert.Equal(new Coordinate("abc", "def"), this.model.Coordinate);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("BRI 123.456 abc def", this.model.GetCompileData(new SectorElementCollection()));
        }
    }
}
