using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class SectorlineCoordinateTest
    {
        private readonly SectorlineCoordinate model;

        public SectorlineCoordinateTest()
        {
            this.model = new SectorlineCoordinate(
                new Coordinate("abc", "def"),
                "comment"
            );
        }

        [Fact]
        public void TestItSetsCoordinate()
        {
            Assert.Equal(new Coordinate("abc", "def"), this.model.Corodinate);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "COORD:abc:def ;comment\r\n",
                this.model.Compile()
            );
        }
    }
}