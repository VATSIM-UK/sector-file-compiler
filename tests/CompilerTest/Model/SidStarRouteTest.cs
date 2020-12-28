using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class SidStarRouteTest
    {
        private readonly SidStarRoute sidStar;

        private List<RouteSegment> segments;

        private RouteSegment initialSegment;

        public SidStarRouteTest()
        {
            this.initialSegment = RouteSegmentFactory.MakeDoublePoint();
            this.segments.Add(RouteSegmentFactory.MakeDoublePoint());
            this.segments.Add(RouteSegmentFactory.MakeDoublePoint());
            this.segments.Add(RouteSegmentFactory.MakeDoublePoint());
            this.segments = new List<RouteSegment>();
            this.sidStar = new SidStarRoute(
                SidStarType.SID,
                "EGKK - ADMAG2X",
                this.initialSegment,
                this.segments,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsIdentifier()
        {
            Assert.Equal("EGKK - ADMAG2X", this.sidStar.Identifier);
        }

        [Fact]
        public void TestItSetsSegments()
        {
            Assert.Equal(this.segments, this.sidStar.Segments);
        }

        [Fact]
        public void TestItCompilesWithPadding()
        {
            string expected = "EGKK - ADMAG2X             LAM LAM BIG BIG";

            Assert.Equal(
                expected,
                this.sidStar.GetCompileData(new SectorElementCollection())
            );
        }

        [Fact]
        public void TestItItMatchesPaddingOnLongerNames()
        {
            SidStarRoute longSidStar = new SidStarRoute(
                SidStarType.SID,
                "This is a long name which needs extra padding",
                this.initialSegment,
                this.segments,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );

            string expected = "This is a long name which needs extra padding LAM LAM BIG BIG";

            Assert.Equal(
                expected,
                longSidStar.GetCompileData(new SectorElementCollection())
            );
        }
    }
}
