using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class RouteSegmentTest
    {
        private readonly RouteSegment segment;

        public RouteSegmentTest()
        {
            this.segment = new RouteSegment(
                "FOO",
                new Point("BIG"),
                new Point("LAM"),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsStart()
        {
            Assert.Equal(new Point("BIG"), this.segment.Start);
        }

        [Fact]
        public void TestItSetsEnd()
        {
            Assert.Equal(new Point("LAM"), this.segment.End);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "                           BIG BIG LAM LAM",
                this.segment.GetCompileData(new SectorElementCollection())
            );
        }

        [Fact]
        public void TestItCompilesWithColour()
        {
            RouteSegment routeSegment = new(
                "FOO",
                new Point("BIG"),
                new Point("LAM"),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make(),
                "FooColour"
            );

            Assert.Equal(
                "                           BIG BIG LAM LAM FooColour",
                routeSegment.GetCompileData(new SectorElementCollection())
            );
        }
    }
}
