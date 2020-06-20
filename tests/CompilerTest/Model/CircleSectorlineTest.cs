using Xunit;
using Compiler.Model;
using System.Collections.Generic;

namespace CompilerTest.Model
{
    public class CircleSectorlineTest
    {
        private readonly CircleSectorline model1;
        private readonly CircleSectorline model2;
        private readonly List<SectorlineDisplayRule> displayRules = new List<SectorlineDisplayRule>
        {
            new SectorlineDisplayRule("TEST1", "TEST1", "TEST2", "comment1"),
            new SectorlineDisplayRule("TEST2", "TEST2", "TEST1", "comment2")
        };

        public CircleSectorlineTest()
        {
            this.model1 = new CircleSectorline(
                "Test Sectorline",
                "EGGD",
                5.5,
                this.displayRules,
                "commentname"
            );

            this.model2 = new CircleSectorline(
                "Test Sectorline",
                new Coordinate("abc", "def"),
                5.5,
                this.displayRules,
                "commentname"
            );
        }

        [Fact]
        public void TestPointVersionSetsName()
        {
            Assert.Equal("Test Sectorline", this.model1.Name);
        }

        [Fact]
        public void TestPointVersionSetsCentrePoint()
        {
            Assert.Equal("EGGD", this.model1.CentrePoint);
        }

        [Fact]
        public void TestPointVersionSetsRadius()
        {
            Assert.Equal(5.5, this.model1.Radius);
        }

        [Fact]
        public void TestPointVersionSetsDisplayRules()
        {
            Assert.Equal(this.displayRules, this.model1.DisplayRules);
        }

        [Fact]
        public void TestCoordinateVersionSetsName()
        {
            Assert.Equal("Test Sectorline", this.model2.Name);
        }

        [Fact]
        public void TestCoordinateVersionSetsCentreCoordinate()
        {
            Assert.Equal(new Coordinate("abc", "def"), this.model2.CentreCoordinate);
        }

        [Fact]
        public void TestCoordinateVersionSetsRadius()
        {
            Assert.Equal(5.5, this.model2.Radius);
        }

        [Fact]
        public void TestCoordinateVersionSetsDisplayRules()
        {
            Assert.Equal(this.displayRules, this.model2.DisplayRules);
        }

        [Fact]
        public void TestPointVersionCompiles()
        {
            Assert.Equal(
                "CIRCLE_SECTORLINE:Test Sectorline:EGGD:5.5 ;commentname\r\nDISPLAY:TEST1:TEST1:TEST2 ;comment1\r\n" +
                    "DISPLAY:TEST2:TEST2:TEST1 ;comment2\r\n\r\n",
                this.model1.Compile()
            );
        }

        [Fact]
        public void TestCoordinateVersionCompiles()
        {
            Assert.Equal(
                "CIRCLE_SECTORLINE:Test Sectorline:abc:def:5.5 ;commentname\r\nDISPLAY:TEST1:TEST1:TEST2 ;comment1\r\n" +
                    "DISPLAY:TEST2:TEST2:TEST1 ;comment2\r\n\r\n",
                this.model2.Compile()
            );
        }
    }
}