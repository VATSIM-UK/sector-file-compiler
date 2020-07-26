using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class RunwayTest
    {
        private readonly Runway runway;

        public RunwayTest()
        {
            this.runway = new Runway(
                "09",
                90,
                new Coordinate("abc", "def"),
                "27",
                270,
                new Coordinate("ghi", "jkl"),
                "EGGD - Bristol",
                "comment"
            );
        }

        [Fact]
        public void TestItSetsFirstIdentifier()
        {
            Assert.Equal("09", this.runway.FirstIdentifier);
        }

        [Fact]
        public void TestItSetsFirstHeading()
        {
            Assert.Equal(90, this.runway.FirstHeading);
        }

        [Fact]
        public void TestItSetsFirstThreshold()
        {
            Assert.Equal(new Coordinate("abc", "def"), this.runway.FirstThreshold);
        }

        [Fact]
        public void TestItSetsReverseIdentifier()
        {
            Assert.Equal("27", this.runway.ReverseIdentifier);
        }

        [Fact]
        public void TestItSetsReserveHeading()
        {
            Assert.Equal(270, this.runway.ReverseHeading);
        }

        [Fact]
        public void TestItSetsReverseThreshold()
        {
            Assert.Equal(new Coordinate("ghi", "jkl"), this.runway.ReverseThreshold);
        }

        [Fact]
        public void TestItSetsRunwayDialogDescription()
        {
            Assert.Equal("EGGD - Bristol", this.runway.RunwayDialogDescription);
        }


        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "09 27 090 270 abc def ghi jkl EGGD - Bristol ;comment\r\n",
                this.runway.Compile()
            );
        }
    }
}
