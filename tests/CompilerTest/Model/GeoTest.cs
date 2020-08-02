using Xunit;
using Compiler.Model;
using System.Collections.Generic;

namespace CompilerTest.Model
{
    public class GeoTest
    {
        private readonly Geo model;
        private readonly List<GeoSegment> segments;

        public GeoTest()
        {
            this.segments = new List<GeoSegment>
            {
                new GeoSegment(
                    new Point(new Coordinate("abc", "def")),
                    new Point(new Coordinate("ghi", "jkl")),
                    "red",
                    "comment1"
                ),
                new GeoSegment(
                    new Point(new Coordinate("mno", "pqr")),
                    new Point(new Coordinate("stu", "vwx")),
                    "blue",
                    "comment2"
                ),
            };
            this.model = new Geo(
                "TestGeo",
                segments
            );
        }

        [Fact]
        public void TestItSetsName()
        {
            Assert.Equal("TestGeo", this.model.Name);
        }

        [Fact]
        public void TestItSetsSegments()
        {
            Assert.Equal(this.segments, this.model.Segments);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "TestGeo                    abc def ghi jkl red ;comment1\r\nmno pqr stu vwx blue ;comment2\r\n",
                this.model.Compile()
            );
        }
    }
}
