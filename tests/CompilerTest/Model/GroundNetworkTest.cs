using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class GroundNetworkTest
    {
        private readonly GroundNetwork network;
        private readonly GroundNetworkTaxiway taxiway1;
        private readonly GroundNetworkTaxiway taxiway2;
        private readonly GroundNetworkRunwayExit exit1;
        private readonly GroundNetworkRunwayExit exit2;
        private List<GroundNetworkRunwayExit> exits;
        private List<GroundNetworkTaxiway> taxiways;

        public GroundNetworkTest()
        {
            taxiway1 = GroundNetworkTaxiwayFactory.Make();
            taxiway2 = GroundNetworkTaxiwayFactory.Make();
            taxiways = new List<GroundNetworkTaxiway>
            {
                taxiway1,
                taxiway2
            };
            exit1 = GroundNetworkRunwayExitFactory.Make();
            exit2 = GroundNetworkRunwayExitFactory.Make();
            exits = new List<GroundNetworkRunwayExit>
            {
                exit1,
                exit2
            };
            
            network = new GroundNetwork(
                "EGLL",
                taxiways,
                exits
            );
        }

        [Fact]
        public void TestItSetsAirport()
        {
            Assert.Equal("EGLL", network.Airport);
        }
        
        [Fact]
        public void TestItSetsTaxiways()
        {
            Assert.Equal(taxiways, network.Taxiways);
        }
        
        [Fact]
        public void TestItSetsRunwayExits()
        {
            Assert.Equal(exits, network.RunwayExits);
        }

        [Fact]
        public void TestItReturnsCompilableElements()
        {
            var expected = new List<ICompilableElement>
            {
                taxiway1,
                taxiway1.Coordinates[0],
                taxiway1.Coordinates[1],
                taxiway1.Coordinates[2],
                taxiway2,
                taxiway2.Coordinates[0],
                taxiway2.Coordinates[1],
                taxiway2.Coordinates[2],
                exit1,
                exit1.Coordinates[0],
                exit1.Coordinates[1],
                exit1.Coordinates[2],
                exit2,
                exit2.Coordinates[0],
                exit2.Coordinates[1],
                exit2.Coordinates[2],
            };
            
            Assert.Equal(expected, network.GetCompilableElements());
        }
    }
}
