using System;
using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

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
                1,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
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
                "ACTIVE_RUNWAY:EGBB:33:1",
                this.activeRunway.GetCompileData(new SectorElementCollection())
            );
        }

        [Theory]
        [InlineData("ABC", "DEF", 0, false)] // All different
        [InlineData("EGBB", "34", 1, false)] // Different identifier
        [InlineData("EGCC", "33", 1, false)] // Different airport
        [InlineData("EGBB", "33", 0, false)] // Different mode
        [InlineData("EGBB", "33", 1, true)] // All the same
        public void TestEquality(string icao, string identifier, int mode, bool expected)
        {
            Assert.Equal(expected, activeRunway.Equals(ActiveRunwayFactory.Make(icao, identifier, mode)));
        }

        [Fact]
        public void TestHash()
        {
            Assert.Equal(HashCode.Combine("33", "EGBB", 1), activeRunway.GetHashCode());
        }
    }
}
