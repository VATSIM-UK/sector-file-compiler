using System.Collections.Generic;
using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class AirportTest
    {
        private readonly Airport airport;

        public AirportTest()
        {
            this.airport = new Airport(
                "Testville",
                "EGTT",
                new Coordinate("abc", "def"),
                "123.456",
                null
            );
        }

        [Fact]
        public void TestItSetsName()
        {
            Assert.Equal("Testville", this.airport.Name);
        }

        [Fact]
        public void TestItSetsIcao()
        {
            Assert.Equal("EGTT", this.airport.Icao);
        }

        [Fact]
        public void TestItSetsLatLong()
        {
            Assert.Equal("abc def", this.airport.LatLong.ToString());
        }

        [Fact]
        public void TestItSetsFrequency()
        {
            Assert.Equal("123.456", this.airport.Frequency.ToString());
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "EGTT 123.456 abc def E ;Testville\r\n",
                this.airport.Compile()
            );
        }

        [Fact]
        public void TestItCompilesWithComment()
        {
            Airport airport = new Airport(
                "Testville",
                "EGTT",
                new Coordinate("abc", "def"),
                "123.456",
                "comment"
            );

            Assert.Equal(
                "EGTT 123.456 abc def E ;comment - Testville\r\n",
                airport.Compile()
            );
        }
    }
}
