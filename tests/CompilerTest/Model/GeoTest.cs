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
                new Point("abc"),
                new Point("def"),
                "Blue",
                "comment"
            );
        }

        [Fact]
        public void TestItSetsStartPoint()
        {
            Assert.Equal(new Point("abc"), this.model.StartPoint);
        }

        [Fact]
        public void TestItSetsEndPoint()
        {
            Assert.Equal(new Point("def"), this.model.EndPoint);
        }

        [Fact]
        public void TestItSetsColour()
        {
            Assert.Equal("Blue", this.model.Colour);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("abc abc def def Blue ;comment\r\n", this.model.Compile());
        }
    }
}
