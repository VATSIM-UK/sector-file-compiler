using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class GroundNetworkParserTest: AbstractParserTestCase
    {
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "COORD:N054.39.27.000:W006.12.57.000"
            }}, // Invalid data type
            new object[] { new List<string>{
                "EXIT:27L:N3W:LEFT:15:16"
            }}, // Too many segments
            new object[] { new List<string>{
                "EXIT:27L:N3W:LEFT"
            }}, // Too few segments
            new object[] { new List<string>{
                "EXIT:ABC:N3W:LEFT:15"
            }}, // Invalid runway
            new object[] { new List<string>{
                "EXIT:27L:N3W:UP:15"
            }}, // Invalid direction
            new object[] { new List<string>{
                "EXIT:27L:N3W:LEFT:abc"
            }}, // Invalid speed
            new object[] { new List<string>{
                "EXIT:27L:N3W:LEFT:0"
            }}, // Speed to low
            new object[] { new List<string>{
                "EXIT:27L:N3W:LEFT:-1"
            }}, // Speed negative
            new object[] { new List<string>{
                "EXIT:27L:N3W:LEFT:15"
            }}, // No coordinates for exit
            new object[] { new List<string>{
                "EXIT:27L:N3W:LEFT:15",
                "COORD:N054.39.27.000:W006.12.57.000:def"
            }}, // Too many COORD segments
            new object[] { new List<string>{
                "EXIT:27L:N3W:LEFT:15",
                "COORD:N054.39.27.000"
            }}, // To few COORD segments
            new object[] { new List<string>{
                "EXIT:27L:N3W:LEFT:15",
                "NOTCOORD:N054.39.27.000:W006.12.57.000"
            }}, // Not a coordinate declaration for exit
            new object[] { new List<string>{
                "EXIT:27L:N3W:LEFT:15",
                "COORD:Nabc.39.27.000:W006.12.57.000"
            }}, // COORD declaration invalid coordinates
            new object[] { new List<string>{
                "TAXI:A"
            }}, // Too few TAXI segments
            new object[] { new List<string>{
                "TAXI:A:15:1:54L:abc"
            }}, // Too many TAXI segments
            new object[] { new List<string>{
                "TAXI:A:a:1:54L"
            }}, // Maximum speed invalid
            new object[] { new List<string>{
                "TAXI:A:a:0:54L"
            }}, // Maximum speed too low
            new object[] { new List<string>{
                "TAXI:A:-1:1:54L"
            }}, // Maximum speed negative
            new object[] { new List<string>{
                "TAXI:A:15:1:54L"
            }}, // Too few TAXI segments
            new object[] { new List<string>{
                "TAXI:A:15:abc:54L"
            }}, // Usage flag not integer
            new object[] { new List<string>{
                "TAXI:A:15:0:54L"
            }}, // Usage flag too low
            new object[] { new List<string>{
                "TAXI:A:15:4:54L"
            }}, // Usage flag too high
            new object[] { new List<string>{
                "TAXI:A:15:1:54L"
            }}, // No coordinates
            new object[] { new List<string>{
                "EXIT:26L:A1:LEFT:15;comment", 
                "TAXI:A1:15:1:25L ;comment3",
                "COORD:N050.57.00.000:W001.21.24.490 ;comment4",
                "COORD:N050.57.00.000:W001.21.24.491 ;comment5"
            }} // First declaration in multi-line bad
        };

        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            RunParserOnLines(lines);

            Assert.Empty(sectorElementCollection.GroundNetworks);
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Theory]
        [InlineData("EXIT:26L:A1:LEFT:15;comment", "LEFT")]
        [InlineData("EXIT:26L:A1:RIGHT:15;comment", "RIGHT")]
        public void TestItAddsRunwayExits(string exitLine, string expectedDirection)
        {
            RunParserOnLines(
                new List<string>()
                {
                    exitLine,
                    "COORD:N050.57.00.000:W001.21.24.490 ;comment1",
                    "COORD:N050.57.00.000:W001.21.24.491 ;comment2"
                }
            );

            Assert.Single(sectorElementCollection.GroundNetworks);
            Assert.Equal("TESTFOLDER", sectorElementCollection.GroundNetworks[0].Airport);
            Assert.Single(sectorElementCollection.GroundNetworks[0].RunwayExits);

            GroundNetworkRunwayExit exit = sectorElementCollection.GroundNetworks[0].RunwayExits[0];
            Assert.Equal("26L", exit.Runway);
            Assert.Equal("A1", exit.ExitName);
            Assert.Equal(expectedDirection, exit.Direction);
            Assert.Equal(15, exit.MaximumSpeed);
            Assert.Equal(2, exit.Coordinates.Count);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), exit.Coordinates[0].Coordinate);
            AssertExpectedMetadata(exit.Coordinates[0], 2, "comment1");
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.491"), exit.Coordinates[1].Coordinate);
            AssertExpectedMetadata(exit.Coordinates[1], 3, "comment2");
            AssertExpectedMetadata(exit);
        }

        [Theory]
        [InlineData("TAXI:A1:15:1:25L ;comment", "A1", 15, 1, "25L")] // All data present
        [InlineData("TAXI:A1:15 ;comment", "A1", 15, null, null)] // No usage flag or gate
        [InlineData("TAXI:A1:15:1 ;comment", "A1", 15, 1, null)] // No gate
        [InlineData("TAXI:A1:15::25L ;comment", "A1", 15, null, "25L")] // Usage flag empty
        [InlineData("TAXI:A1:15:1: ;comment", "A1", 15, 1, null)] // Gate empty
        [InlineData("TAXI:A1:15:: ;comment", "A1", 15, null, null)] // Gate and usage flag empty
        public void TestItAddsTaxiways(
            string taxiwayLine,
            string expectedName,
            int expectedMaximumSpeed,
            int? expectedUsageFlag,
            string expectedGateName
        ) {
            RunParserOnLines(
                new List<string>
                {
                    taxiwayLine,
                    "COORD:N050.57.00.000:W001.21.24.490 ;comment1",
                    "COORD:N050.57.00.000:W001.21.24.491 ;comment2"
                }
            );
            
            Assert.Single(sectorElementCollection.GroundNetworks);
            Assert.Equal("TESTFOLDER", sectorElementCollection.GroundNetworks[0].Airport);
            Assert.Single(sectorElementCollection.GroundNetworks[0].Taxiways);

            GroundNetworkTaxiway taxiway = sectorElementCollection.GroundNetworks[0].Taxiways[0];
            Assert.Equal(expectedName, taxiway.Name);
            Assert.Equal(expectedMaximumSpeed, taxiway.MaximumSpeed);
            Assert.Equal(expectedUsageFlag, taxiway.UsageFlag);
            Assert.Equal(expectedGateName, taxiway.GateName);
            Assert.Equal(2, taxiway.Coordinates.Count);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), taxiway.Coordinates[0].Coordinate);
            AssertExpectedMetadata(taxiway.Coordinates[0], 2, "comment1");
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.491"), taxiway.Coordinates[1].Coordinate);
            AssertExpectedMetadata(taxiway.Coordinates[1], 3, "comment2");
            AssertExpectedMetadata(taxiway);
        }

        [Fact]
        public void TestItAddsMultipleElements()
        {
            RunParserOnLines(
                new List<string>()
                {
                    "EXIT:26L:A1:123:15;comment",
                    "COORD:N050.57.00.000:W001.21.24.490 ;comment1",
                    "COORD:N050.57.00.000:W001.21.24.491 ;comment2",
                    "TAXI:A1:15:1:25L ;comment3",
                    "COORD:N050.57.00.000:W001.21.24.490 ;comment4",
                    "COORD:N050.57.00.000:W001.21.24.491 ;comment5"
                }
            );
            Assert.Single(sectorElementCollection.GroundNetworks);
            Assert.Single(sectorElementCollection.GroundNetworks[0].Taxiways);
            Assert.Single(sectorElementCollection.GroundNetworks[0].RunwayExits);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.ESE_GROUND_NETWORK;
        }
    }
}
