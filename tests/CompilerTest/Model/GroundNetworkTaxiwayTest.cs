using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class GroundNetworkTaxiwayTest
    {
        private readonly GroundNetworkTaxiway taxiway;
        private List<GroundNetworkCoordinate> coordinates;
        private GroundNetworkCoordinate coordinate1;
        private GroundNetworkCoordinate coordinate2;

        public GroundNetworkTaxiwayTest()
        {
            coordinate1 = GroundNetworkCoordinateFactory.Make(new Coordinate("abc", "def"));
            coordinate2 = GroundNetworkCoordinateFactory.Make(new Coordinate("abc", "ghi"));
            coordinates = new List<GroundNetworkCoordinate>
            {
                coordinate1,
                coordinate2
            };
            taxiway = new GroundNetworkTaxiway(
                "A",
                15,
                1,
                "55L",
                coordinates,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsName()
        {
            Assert.Equal("A", taxiway.Name);
        }
        
        [Fact]
        public void TestItSetsMaximumSpeed()
        {
            Assert.Equal(15, taxiway.MaximumSpeed);
        }
        
        [Fact]
        public void TestItSetsUsageFlag()
        {
            Assert.Equal(1, taxiway.UsageFlag);
        }
        
        [Fact]
        public void TestItSetsGateName()
        {
            Assert.Equal("55L", taxiway.GateName);
        }
        
        [Fact]
        public void TestItSetsCoordinates()
        {
            Assert.Equal(coordinates, taxiway.Coordinates);
        }
        
        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("TAXI:A:15:1:55L", taxiway.GetCompileData(new SectorElementCollection()));
        }

        [Fact]
        public void TestItReturnsCompilableElements()
        {
            var expected = new List<ICompilableElement>
            {
                taxiway,
                coordinate1,
                coordinate2
            };
            Assert.Equal(expected, taxiway.GetCompilableElements());
        }
        
        [Fact]
        public void TestItCompilesWithNulls()
        {
            var taxiway2 = new GroundNetworkTaxiway(
                "A",
                15,
                null,
                null,
                coordinates,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            Assert.Equal("TAXI:A:15::", taxiway2.GetCompileData(new SectorElementCollection()));
        }
    }
}
