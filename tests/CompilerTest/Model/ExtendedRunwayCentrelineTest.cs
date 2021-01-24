using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class ExtendedRunwayCentrelineTest
    {
        private readonly ExtendedRunwayCentreline centreline;
        private readonly RunwayCentrelineSegment segment;

        public ExtendedRunwayCentrelineTest()
        {
            segment = RunwayCentrelineSegmentFactory.Make();
            centreline = new ExtendedRunwayCentreline(
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
            Assert.Equal(
                $"{segment} centrelinecolour",
                centreline.GetCompileData(new SectorElementCollection())
            );
        }
    }
}
