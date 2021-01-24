using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class FixedColourRunwayCentrelineTest
    {
        private readonly FixedColourRunwayCentreline centreline;
        private readonly RunwayCentrelineSegment segment;

        public FixedColourRunwayCentrelineTest()
        {
            segment = RunwayCentrelineSegmentFactory.Make();
            centreline = new FixedColourRunwayCentreline(
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
