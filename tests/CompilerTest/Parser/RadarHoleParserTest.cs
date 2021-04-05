using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class RadarHoleParserTest: AbstractParserTestCase
    {
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "HOLE:1:2:3:4"
            }}, // Too many sections
            new object[] { new List<string>{
                "HOLE:1:2"
            }}, // Too few sections
            new object[] { new List<string>{
                "HOLE2:1:2:3"
            }}, // Invalid declaration
            new object[] { new List<string>{
                "HOLE:::"
            }}, // No data provided
            new object[] { new List<string>{
                "HOLE:a:2:3"
            }}, // Invalid primary top
            new object[] { new List<string>{
                "HOLE:1:b:3"
            }}, // Invalid s mode top
            new object[] { new List<string>{
                "HOLE:1:2:c"
            }}, // Invalid c mode top
            new object[] { new List<string>{
                "HOLE:1:2:3",
                "COORD:N050.57.00.000"
            }}, // Too few coordinate segments
            new object[] { new List<string>{
                "HOLE:1:2:3",
                "COORD:N050.57.00.000:W001.21.24.490:abc"
            }}, // Too many coordinate segments
            new object[] { new List<string>{
                "HOLE:1:2:3",
                "COORD2:N050.57.00.000:W001.21.24.490"
            }}, // Bad COORD declaration
            new object[] { new List<string>{
                "HOLE:1:2:3",
                "COORD:W050.57.00.000:W001.21.24.490"
            }}, // Invalid COORD
            new object[] { new List<string>{
                "HOLE:1:2:3",
                "COORD:N050.57.00.000:W001.21.24.490"
            }}, // Only one COORD
            new object[] { new List<string>{
                "HOLE:1:2:3",
                "COORD:N050.57.00.000:W001.21.24.490",
                "COORD:N050.57.00.000:W001.21.24.490"
            }}, // Only two COORDs
        };

        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            RunParserOnLines(lines);

            Assert.Empty(sectorElementCollection.Radars);
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsHoleDataAllPresent()
        {
            var lines = new List<string>
            {
                "HOLE:1:2:3;comment",
                "COORD:N050.57.00.000:W001.21.24.491;comment1",
                "COORD:N050.57.00.000:W001.21.24.492;comment2",
                "COORD:N050.57.00.000:W001.21.24.493;comment3"
            };

            RunParserOnLines(lines);

            Assert.Single(sectorElementCollection.RadarHoles);
            RadarHole result = sectorElementCollection.RadarHoles[0];
            AssertExpectedMetadata(result);
            
            // Tops
            Assert.Equal(1, result.PrimaryTop);
            Assert.Equal(2, result.SModeTop);
            Assert.Equal(3, result.CModeTop);
            
            // Coordinate 1
            Assert.Equal(3, result.Coordinates.Count);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.491"), result.Coordinates[0].Coordinate);
            AssertExpectedMetadata(result.Coordinates[0], 2, "comment1");
            
            // Coordinate 2
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.492"), result.Coordinates[1].Coordinate);
            AssertExpectedMetadata(result.Coordinates[1], 3, "comment2");
            
            // Coordinate 3
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.493"), result.Coordinates[2].Coordinate);
            AssertExpectedMetadata(result.Coordinates[2], 4, "comment3");
        }
        
        [Fact]
        public void TestItAddsHoleDataPrimaryMissing()
        {
            var lines = new List<string>
            {
                "HOLE::2:3;comment",
                "COORD:N050.57.00.000:W001.21.24.491;comment1",
                "COORD:N050.57.00.000:W001.21.24.492;comment2",
                "COORD:N050.57.00.000:W001.21.24.493;comment3"
            };

            RunParserOnLines(lines);

            Assert.Single(sectorElementCollection.RadarHoles);
            RadarHole result = sectorElementCollection.RadarHoles[0];
            AssertExpectedMetadata(result);
            
            // Tops
            Assert.Null(result.PrimaryTop);
            Assert.Equal(2, result.SModeTop);
            Assert.Equal(3, result.CModeTop);
            
            // Coordinate 1
            Assert.Equal(3, result.Coordinates.Count);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.491"), result.Coordinates[0].Coordinate);
            AssertExpectedMetadata(result.Coordinates[0], 2, "comment1");
            
            // Coordinate 2
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.492"), result.Coordinates[1].Coordinate);
            AssertExpectedMetadata(result.Coordinates[1], 3, "comment2");
            
            // Coordinate 3
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.493"), result.Coordinates[2].Coordinate);
            AssertExpectedMetadata(result.Coordinates[2], 4, "comment3");
        }
        
        [Fact]
        public void TestItAddsHoleDataSModeMissing()
        {
            var lines = new List<string>
            {
                "HOLE:1::3;comment",
                "COORD:N050.57.00.000:W001.21.24.491;comment1",
                "COORD:N050.57.00.000:W001.21.24.492;comment2",
                "COORD:N050.57.00.000:W001.21.24.493;comment3"
            };

            RunParserOnLines(lines);

            Assert.Single(sectorElementCollection.RadarHoles);
            RadarHole result = sectorElementCollection.RadarHoles[0];
            AssertExpectedMetadata(result);
            
            // Tops
            Assert.Equal(1, result.PrimaryTop);
            Assert.Null(result.SModeTop);
            Assert.Equal(3, result.CModeTop);
            
            // Coordinate 1
            Assert.Equal(3, result.Coordinates.Count);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.491"), result.Coordinates[0].Coordinate);
            AssertExpectedMetadata(result.Coordinates[0], 2, "comment1");
            
            // Coordinate 2
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.492"), result.Coordinates[1].Coordinate);
            AssertExpectedMetadata(result.Coordinates[1], 3, "comment2");
            
            // Coordinate 3
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.493"), result.Coordinates[2].Coordinate);
            AssertExpectedMetadata(result.Coordinates[2], 4, "comment3");
        }
        
        [Fact]
        public void TestItAddsHoleDataCModeMissing()
        {
            var lines = new List<string>
            {
                "HOLE:1:2:;comment",
                "COORD:N050.57.00.000:W001.21.24.491;comment1",
                "COORD:N050.57.00.000:W001.21.24.492;comment2",
                "COORD:N050.57.00.000:W001.21.24.493;comment3"
            };

            RunParserOnLines(lines);

            Assert.Single(sectorElementCollection.RadarHoles);
            RadarHole result = sectorElementCollection.RadarHoles[0];
            AssertExpectedMetadata(result);
            
            // Tops
            Assert.Equal(1, result.PrimaryTop);
            Assert.Equal(2, result.SModeTop);
            Assert.Null(result.CModeTop);
            
            // Coordinate 1
            Assert.Equal(3, result.Coordinates.Count);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.491"), result.Coordinates[0].Coordinate);
            AssertExpectedMetadata(result.Coordinates[0], 2, "comment1");
            
            // Coordinate 2
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.492"), result.Coordinates[1].Coordinate);
            AssertExpectedMetadata(result.Coordinates[1], 3, "comment2");
            
            // Coordinate 3
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.493"), result.Coordinates[2].Coordinate);
            AssertExpectedMetadata(result.Coordinates[2], 4, "comment3");
        }
        
        [Fact]
        public void TestItAddsMultipleHoles()
        {
            var lines = new List<string>
            {
                "HOLE:1:2:3;comment",
                "COORD:N050.57.00.000:W001.21.24.491;comment1",
                "COORD:N050.57.00.000:W001.21.24.492;comment2",
                "COORD:N050.57.00.000:W001.21.24.493;comment3",
                "HOLE:4:5:6;comment4",
                "COORD:N050.57.00.000:W001.21.24.494;comment5",
                "COORD:N050.57.00.000:W001.21.24.495;comment6",
                "COORD:N050.57.00.000:W001.21.24.496;comment7"
            };

            RunParserOnLines(lines);

            Assert.Equal(2, sectorElementCollection.RadarHoles.Count);
            
            // The first result
            RadarHole result1 = sectorElementCollection.RadarHoles[0];
            AssertExpectedMetadata(result1);
            
            // Tops
            Assert.Equal(1, result1.PrimaryTop);
            Assert.Equal(2, result1.SModeTop);
            Assert.Equal(3, result1.CModeTop);
            
            // Coordinate 1
            Assert.Equal(3, result1.Coordinates.Count);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.491"), result1.Coordinates[0].Coordinate);
            AssertExpectedMetadata(result1.Coordinates[0], 2, "comment1");
            
            // Coordinate 2
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.492"), result1.Coordinates[1].Coordinate);
            AssertExpectedMetadata(result1.Coordinates[1], 3, "comment2");
            
            // Coordinate 3
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.493"), result1.Coordinates[2].Coordinate);
            AssertExpectedMetadata(result1.Coordinates[2], 4, "comment3");
            
            // The second result
            RadarHole result2 = sectorElementCollection.RadarHoles[1];
            AssertExpectedMetadata(result2, 5, "comment4");
            
            // Tops
            Assert.Equal(4, result2.PrimaryTop);
            Assert.Equal(5, result2.SModeTop);
            Assert.Equal(6, result2.CModeTop);
            
            // Coordinate 1
            Assert.Equal(3, result2.Coordinates.Count);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.494"), result2.Coordinates[0].Coordinate);
            AssertExpectedMetadata(result2.Coordinates[0], 6, "comment5");
            
            // Coordinate 2
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.495"), result2.Coordinates[1].Coordinate);
            AssertExpectedMetadata(result2.Coordinates[1], 7, "comment6");
            
            // Coordinate 3
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.496"), result2.Coordinates[2].Coordinate);
            AssertExpectedMetadata(result2.Coordinates[2], 8, "comment7");
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.ESE_RADAR_HOLE;
        }
    }
}
