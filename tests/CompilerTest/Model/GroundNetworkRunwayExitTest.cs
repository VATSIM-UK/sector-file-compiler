using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class GroundNetworkRunwayExitTest
    {
        private readonly GroundNetworkRunwayExit exit;
        private List<GroundNetworkCoordinate> coordinates;
        private GroundNetworkCoordinate coordinate1;
        private GroundNetworkCoordinate coordinate2;

        public GroundNetworkRunwayExitTest()
        {
            coordinate1 = GroundNetworkCoordinateFactory.Make(new Coordinate("abc", "def"));
            coordinate2 = GroundNetworkCoordinateFactory.Make(new Coordinate("abc", "ghi"));
            coordinates = new List<GroundNetworkCoordinate>
            {
                coordinate1,
                coordinate2
            };
            exit = new GroundNetworkRunwayExit(
                "27L",
                "N3W",
                "LEFT",
                15,
                coordinates,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsRunway()
        {
            Assert.Equal("27L", exit.Runway);
        }
        
        [Fact]
        public void TestItSetsExitName()
        {
            Assert.Equal("N3W", exit.ExitName);
        }
        
        [Fact]
        public void TestItSetsDirection()
        {
            Assert.Equal("LEFT", exit.Direction);
        }
        
        [Fact]
        public void TestItSetsMaximumSpeed()
        {
            Assert.Equal(15, exit.MaximumSpeed);
        }
        
        [Fact]
        public void TestItSetsCoordinates()
        {
            Assert.Equal(coordinates, exit.Coordinates);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("EXIT:27L:N3W:LEFT:15", exit.GetCompileData(new SectorElementCollection()));
        }

        [Fact]
        public void TestItReturnsCompilableElements()
        {
            var expected = new List<ICompilableElement>
            {
                exit,
                coordinate1,
                coordinate2
            };
            Assert.Equal(expected, exit.GetCompilableElements());
        }
    }
}
