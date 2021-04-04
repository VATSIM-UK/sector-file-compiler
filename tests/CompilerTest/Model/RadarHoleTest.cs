using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class RadarHoleTest
    {
        private readonly RadarHole modelNotNull;
        private readonly RadarHole modelNull;

        public RadarHoleTest()
        {
            modelNotNull = new RadarHole(
                1,
                2,
                3,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            modelNull = new RadarHole(
                null,
                null,
                null,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsPrimaryTopNotNull()
        {
            Assert.Equal(1, modelNotNull.PrimaryTop);
        }
        
        [Fact]
        public void TestItSetsPrimaryTopNull()
        {
            Assert.Null(modelNull.PrimaryTop);
        }
        
        [Fact]
        public void TestItSetsSModeTopNotNull()
        {
            Assert.Equal(2, modelNotNull.SModeTop);
        }
        
        [Fact]
        public void TestItSetsSModeTopNull()
        {
            Assert.Null(modelNull.SModeTop);
        }
        
        [Fact]
        public void TestItSetsCModeTopNotNull()
        {
            Assert.Equal(3, modelNotNull.CModeTop);
        }
        
        [Fact]
        public void TestItSetsCModeTopNull()
        {
            Assert.Null(modelNull.CModeTop);
        }

        [Fact]
        public void TestItCompilesNotNull()
        {
            Assert.Equal("HOLE:1:2:3", modelNotNull.GetCompileData(new SectorElementCollection()));
        }
        
        [Fact]
        public void TestItCompilesNull()
        {
            Assert.Equal("HOLE:::", modelNull.GetCompileData(new SectorElementCollection()));
        }
    }
}
