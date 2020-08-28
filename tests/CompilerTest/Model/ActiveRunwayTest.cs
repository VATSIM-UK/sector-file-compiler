using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class ActiveRunwayTest
    {
        private readonly ActiveRunway activeRunway;

        public ActiveRunwayTest()
        {
            this.activeRunway = new ActiveRunway(
                "33",
                "EGBB",
                1
            );
        }

        [Fact]
        public void TestItSetsIdentifier()
        {
            Assert.Equal("33", this.activeRunway.Identifier);
        }

        [Fact]
        public void TestItSetsAirfield()
        {
            Assert.Equal("EGBB", this.activeRunway.Airfield);
        }

        [Fact]
        public void TestItSetsMode()
        {
            Assert.Equal(1, this.activeRunway.Mode);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "ACTIVE_RUNWAY:EGBB:33:1\r\n",
                this.activeRunway.Compile()
            );
        }
    }
}
