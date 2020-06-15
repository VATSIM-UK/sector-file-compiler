using Xunit;
using Compiler.Model;
using System.Collections.Generic;

namespace CompilerTest.Model
{
    public class SectorlineTest
    {
        private readonly Sectorline model;
        private readonly List<SectorlineDisplayRule> displayRules = new List<SectorlineDisplayRule>
        {
            new SectorlineDisplayRule("TEST1", "TEST1", "TEST2", "comment1"),
            new SectorlineDisplayRule("TEST2", "TEST2", "TEST1", "comment2")
        };

        private readonly List<SectorlineCoordinate> coordinates = new List<SectorlineCoordinate>
        {
            new SectorlineCoordinate(new Coordinate("abc", "def"), "comment3"),
            new SectorlineCoordinate(new Coordinate("ghi", "jkl"), "comment4"),
        };

        public SectorlineTest()
        {
            this.model = new Sectorline(
                "Test Sectorline",
                this.displayRules,
                this.coordinates,
                "commentname"
            );
        }

        [Fact]
        public void TestItSetsDisplayRules()
        {
            Assert.Equal(this.displayRules, this.model.DisplayRules);
        }

        [Fact]
        public void TestItSetsCoordinates()
        {
            Assert.Equal(this.coordinates, this.model.Coordinates);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "SECTORLINE:Test Sectorline ;commentname\r\nDISPLAY:TEST1:TEST1:TEST2 ;comment1\r\n" +
                    "DISPLAY:TEST2:TEST2:TEST1 ;comment2\r\nCOORD:abc:def ;comment3\r\nCOORD:ghi:jkl ;comment4\r\n\r\n",
                this.model.Compile()
            );
        }
    }
}