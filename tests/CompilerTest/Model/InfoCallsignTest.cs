using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class InfoCallsignTest
    {
        private readonly InfoCallsign model;

        public InfoCallsignTest()
        {
            this.model = new InfoCallsign(
                "LON_CTR",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsCallsign()
        {
            Assert.Equal("LON_CTR", this.model.Callsign);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "LON_CTR",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}