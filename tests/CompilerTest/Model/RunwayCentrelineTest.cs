using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class RunwayCentrelineTest
    {
        private readonly RunwayCentreline centreline;
        private readonly RunwayCentrelineSegment segment;

        public RunwayCentrelineTest()
        {
            segment = RunwayCentrelineSegmentFactory.Make();
            centreline = new RunwayCentreline(
                segment,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsSegment()
        {
            Assert.Equal(segment, centreline.CentrelineSegment);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(segment.ToString(), centreline.GetCompileData(new SectorElementCollection()));
        }
    }
}
