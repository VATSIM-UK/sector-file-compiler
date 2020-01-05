using System.Collections.Generic;
using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class RouteSegmentTest
    {
        private readonly RouteSegment segment;

        private List<Point> points;

        public RouteSegmentTest()
        {
            this.segment = new RouteSegment(
                new Point("BIG"),
                new Point("LAM"),
                null
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
                "BIG BIG LAM LAM\r\n",
                this.segment.Compile()
            );
        }

        [Fact]
        public void TestItCompilesWithComment()
        {
            RouteSegment segment = new RouteSegment(
                new Point("BIG"),
                new Point("LAM"),
                "Foo"
            );

            Assert.Equal(
                "BIG BIG LAM LAM ;Foo\r\n",
                segment.Compile()
            );
        }
    }
}
