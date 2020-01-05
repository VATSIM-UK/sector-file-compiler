using System.Collections.Generic;
using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class SidStarRouteTest
    {
        private readonly SidStarRoute sidStar;

        private List<RouteSegment> segments;

        public SidStarRouteTest()
        {
            this.segments = new List<RouteSegment>();
            this.sidStar = new SidStarRoute(
                "SID",
                "EGKK - ADMAG2X",
                this.segments
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
            this.segments.Add(new RouteSegment(new Point("LAM"), new Point("BIG"), null));
            this.segments.Add(new RouteSegment(new Point("BIG"), new Point("OCK"), null));
            Assert.Equal(this.segments, this.sidStar.Segments);
        }

        [Fact]
        public void TestItCompiles()
        {
            this.segments.Add(new RouteSegment(new Point("LAM"), new Point("BIG"), null));
            this.segments.Add(new RouteSegment(new Point("BIG"), new Point("OCK"), null));

            string expected = "EGKK - ADMAG2X             LAM LAM BIG BIG\r\n";
            expected += "                           BIG BIG OCK OCK\r\n";

            Assert.Equal(
                expected,
                this.sidStar.Compile()
            );
        }
    }
}
