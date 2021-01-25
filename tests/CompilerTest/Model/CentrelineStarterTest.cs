using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class CentrelineStarterTest
    {
        private readonly RunwayCentrelineSegment segment;

        public CentrelineStarterTest()
        {
            segment = RunwayCentrelineSegmentFactory.Make();
        }

        private CentrelineStarter GetStarter(bool isExtended)
        {
            return new(
                isExtended,
                segment,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }
        

        [Fact]
        public void TestItSetsSegment()
        {
            Assert.Equal(segment, GetStarter(true).CentrelineSegment);
        }

        [Fact]
        public void TestItCompilesWhenExtended()
        {
            Assert.Equal(
                $"Extended centrelines {segment}",
                GetStarter(true).GetCompileData(new SectorElementCollection())
            );
        }
        
        [Fact]
        public void TestItCompilesWhenNotExtended()
        {
            Assert.Equal(
                $"Centrelines {segment}",
                GetStarter(false).GetCompileData(new SectorElementCollection())
            );
        }
    }
}
