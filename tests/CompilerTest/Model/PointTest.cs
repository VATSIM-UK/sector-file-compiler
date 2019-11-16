using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class PointTest
    {
        [Fact]
        public void TestItHasTypeIdentifier()
        {
            Point point = new Point("TESTF");
            Assert.Equal(Point.TYPE_IDENTIFIER, point.Type());
        }

        [Fact]
        public void TestItCompilesIdentifier()
        {
            Point point = new Point("TESTF");
            Assert.Equal("TESTF TESTF", point.Compile());
        }

        [Fact]
        public void TestItHasTypeCoordinate()
        {
            Point point = new Point(new Coordinate("abc", "def"));
            Assert.Equal(Point.TYPE_COORDINATE, point.Type());
        }

        [Fact]
        public void TestItCompilesCoordinate()
        {
            Point point = new Point(new Coordinate("abc", "def"));
            Assert.Equal("abc def", point.Compile());
        }
    }
}
