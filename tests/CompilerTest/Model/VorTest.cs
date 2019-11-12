using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class VorTest
    {
        private readonly Vor model;

        public VorTest()
        {
            this.model = new Vor("BHD", "123.456", new Coordinate("abc", "def"), "comment");
        }

        [Fact]
        public void TestItSetsIdentifier()
        {
            Assert.Equal("BHD", this.model.Identifier);
        }

        [Fact]
        public void TestItSetsFrequency()
        {
            Assert.Equal("123.456", this.model.Frequency);
        }

        [Fact]
        public void TestItSetsCoordinate()
        {
            Assert.Equal(new Coordinate("abc", "def"), this.model.Coordinate);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("BHD 123.456 abc def ;comment\r\n", this.model.Compile());
        }
    }
}
