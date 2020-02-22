using Xunit;
using System.Collections.Generic;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class RegionTest
    {
        private readonly Region region;

        private readonly List<Point> points = new List<Point>();

        public RegionTest()
        {
            this.points.Add(new Point("test1"));
            this.points.Add(new Point("test2"));
            this.points.Add(new Point("test3"));
            this.region = new Region(
                "Red",
                this.points,
                null
            );
        }

        [Fact]
        public void TestItSetsColour()
        {
            Assert.Equal("Red", this.region.Colour);
        }

        [Fact]
        public void TestItSetsPoints()
        {
            Assert.Equal(this.points, this.region.Points);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "Red test1 test1\r\ntest2 test2\r\ntest3 test3\r\n",
                this.region.Compile()
            );
        }

        [Fact]
        public void TestItCompilesWithComment()
        {
            Region region = new Region(
                "Red",
                this.points,
                "comment"
            );

            Assert.Equal(
                "Red test1 test1 ;comment\r\ntest2 test2\r\ntest3 test3\r\n",
                region.Compile()
            );
        }
    }
}
