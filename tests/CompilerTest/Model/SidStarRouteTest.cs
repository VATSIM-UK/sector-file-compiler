using System.Collections.Generic;
using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class SidStarRouteTest
    {
        private readonly SidStarRoute sidStar;

        private List<Point> points;

        public SidStarRouteTest()
        {
            this.points = new List<Point>();
            this.sidStar = new SidStarRoute(
                "EGKK - ADMAG2X",
                this.points,
                null
            );
        }

        [Fact]
        public void TestItSetsIdentifier()
        {
            Assert.Equal("EGKK - ADMAG2X", this.sidStar.Identifier);
        }

        [Fact]
        public void TestItSetsPoints()
        {
            this.points.Add(new Point("LAM"));
            this.points.Add(new Point("BIG"));
            Assert.Equal(this.points, this.sidStar.Points);
        }

        [Fact]
        public void TestItCompiles()
        {
            this.points.Add(new Point("LAM"));
            this.points.Add(new Point("BIG"));
            this.points.Add(new Point("OCK"));

            string expected = "EGKK - ADMAG2X".PadRight(27);
            expected += "LAM LAM BIG BIG\r\n";
            expected = "".PadLeft(27) + "BIG BIG OCK OCK\r\n";

            Assert.Equal(
                expected,
                this.sidStar.Compile()
            );
        }

        [Fact]
        public void TestItCompilesTwoFixes()
        {
            this.points.Add(new Point("LAM"));
            this.points.Add(new Point("BIG"));

            string expected = "EGKK - ADMAG2X".PadRight(27);
            expected += "LAM LAM BIG BIG\r\n";

            Assert.Equal(
                expected,
                this.sidStar.Compile()
            );
        }

        [Fact]
        public void TestItCompilesWithComment()
        {
            this.points.Add(new Point("LAM"));
            this.points.Add(new Point("BIG"));

            string expected = "EGKK - ADMAG2X".PadRight(27);
            expected += "LAM LAM BIG BIG\r\n";

            SidStarRoute sid = new SidStarRoute(
                "EGKK - ADMAG2X",
                this.points,
                "Test"
            );

            Assert.Equal(
                "SID:EGKK:26L:ADMAG2X:FIX1 FIX2 FIX3 ;comment\r\n",
                sid.Compile()
            );
        }
    }
}
