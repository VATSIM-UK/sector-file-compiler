using System.Collections.Generic;
using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class SidStarTest
    {
        private readonly SidStar sidStar;

        public SidStarTest()
        {
            this.sidStar = new SidStar(
                SidStar.TYPE_SID,
                "EGKK",
                "26L",
                "ADMAG2X",
                new List<string>(new string[] { "FIX1", "FIX2", "FIX3" }),
                "a lovely comment"
            );
        }

        [Fact]
        public void TestItSetsType()
        {
            Assert.Equal(SidStar.TYPE_SID, this.sidStar.SidOrStar);
        }

        [Fact]
        public void TestItSetsAirfield()
        {
            Assert.Equal("EGKK", this.sidStar.Airport);
        }

        [Fact]
        public void TestItSetsRunway()
        {
            Assert.Equal("26L", this.sidStar.Runway);
        }

        [Fact]
        public void TestItSetsIdentifier()
        {
            Assert.Equal("ADMAG2X", this.sidStar.Identifier);
        }

        [Fact]
        public void TestItSetsRoute()
        {
            Assert.Equal(new List<string>(new string[] { "FIX1", "FIX2", "FIX3" }), this.sidStar.Route);
        }

        [Fact]
        public void TestItFormatsOutput()
        {
            string expected = "SID:EGKK:26L:ADMAG2X:FIX1 FIX2 FIX3; a lovely comment";
            Assert.Equal(expected, this.sidStar.BuildOutput());
        }
    }
}
