using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class GeoTest
    {
        private readonly Geo model;

        public GeoTest()
        {
            this.model = new Geo(
                new Coordinate("abc", "def"),
                new Coordinate("ghi", "jkl"),
                "Blue",
                "comment"
            );
        }

        [Fact]
        public void TestItSetsStartPoint()
        {
            Assert.Equal(new Coordinate("abc", "def"), this.model.StartPoint);
        }

        [Fact]
        public void TestItSetsEndPoint()
        {
            Assert.Equal(new Coordinate("ghi", "jkl"), this.model.EndPoint);
        }

        [Fact]
        public void TestItSetsColour()
        {
            Assert.Equal("Blue", this.model.Colour);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("abc def ghi jkl Blue ;comment\r\n", this.model.Compile());
        }
    }
}
