using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class RadarParserTest: AbstractParserTestCase
    {
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "RADAR2:Test:N050.57.00.000:W001.21.24.490:1:2:3:4:5:6:7:8:9:10"
            }}, // Too many sections
            new object[] { new List<string>{
                "RADAR2:Test:N050.57.00.000:W001.21.24.490:1:2:3:4:5:6:7:8"
            }}, // Too few sections
            new object[] { new List<string>{
                "RADAR:Test:N050.57.00.000:W001.21.24.490:1:2:3:4:5:6:7:8:9"
            }}, // Old format
            new object[] { new List<string>{
                "RADAR2:Test:W050.57.00.000:W001.21.24.490:1:2:3:4:5:6:7:8:9"
            }}, // Invalid coordinate
            new object[] { new List<string>{
                "RADAR2:Test:N050.57.00.000:W001.21.24.490:::::::::"
            }}, // No type data
            new object[] { new List<string>{
                "RADAR2:Test:N050.57.00.000:W001.21.24.490::2:3:4:5:6:7:8:9"
            }}, // Primary missing data
            new object[] { new List<string>{
                "RADAR2:Test:N050.57.00.000:W001.21.24.490:a:2:3:4:5:6:7:8:9"
            }}, // Invalid primary range
            new object[] { new List<string>{
                "RADAR2:Test:N050.57.00.000:W001.21.24.490:1:b:3:4:5:6:7:8:9"
            }}, // Invalid primary altitude
            new object[] { new List<string>{
                "RADAR2:Test:N050.57.00.000:W001.21.24.490:1:2:c:4:5:6:7:8:9"
            }}, // Invalid primary cone slope
            new object[] { new List<string>{
                "RADAR2:Test:N050.57.00.000:W001.21.24.490:1::3:4:5:6:7:8:9"
            }}, // S mode missing data
            new object[] { new List<string>{
                "RADAR2:Test:N050.57.00.000:W001.21.24.490:1:2:3:a:5:6:7:8:9"
            }}, // S mode invalid range
            new object[] { new List<string>{
                "RADAR2:Test:N050.57.00.000:W001.21.24.490:1:2:3:4:b:6:7:8:9"
            }}, // S mode invalid altitude
            new object[] { new List<string>{
                "RADAR2:Test:N050.57.00.000:W001.21.24.490:1:2:3:4:5:c:7:8:9"
            }}, // S mode invalid cone slope
            new object[] { new List<string>{
                "RADAR2:Test:N050.57.00.000:W001.21.24.490:1:2:3:4:5:6:7:8:"
            }}, // C mode missing data
            new object[] { new List<string>{
                "RADAR2:Test:N050.57.00.000:W001.21.24.490:1:2:3:4:5:6:a:8:9"
            }}, // C mode invalid range
            new object[] { new List<string>{
                "RADAR2:Test:N050.57.00.000:W001.21.24.490:1:2:3:4:5:6:7:b:9"
            }}, // C mode invalid altitude
            new object[] { new List<string>{
                "RADAR2:Test:N050.57.00.000:W001.21.24.490:1:2:3:4:5:6:7:8:c"
            }}, // C mode invalid cone slope
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
        public void TestItAddsRadarDataAllPresent()
        {
            RunParserOnLines(new List<string>() {"RADAR2:Test:N050.57.00.000:W001.21.24.490:1:2:3:4:5:6:7:8:9;comment"});

            Radar result = sectorElementCollection.Radars[0];
            Assert.Equal("Test", result.Name);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Coordinate);
            Assert.Equal(new RadarParameters(1, 2, 3), result.PrimaryRadarParameters);
            Assert.Equal(new RadarParameters(4, 5, 6), result.SModeRadarParameters);
            Assert.Equal(new RadarParameters(7, 8, 9), result.CModeRadarParameters);
            AssertExpectedMetadata(result);
        }
        
        [Fact]
        public void TestItAddsRadarDataPrimaryMissing()
        {
            RunParserOnLines(new List<string>() {"RADAR2:Test:N050.57.00.000:W001.21.24.490::::4:5:6:7:8:9;comment"});

            Radar result = sectorElementCollection.Radars[0];
            Assert.Equal("Test", result.Name);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Coordinate);
            Assert.Equal(new RadarParameters(), result.PrimaryRadarParameters);
            Assert.Equal(new RadarParameters(4, 5, 6), result.SModeRadarParameters);
            Assert.Equal(new RadarParameters(7, 8, 9), result.CModeRadarParameters);
            AssertExpectedMetadata(result);
        }
        
        [Fact]
        public void TestItAddsRadarDataSModeMissing()
        {
            RunParserOnLines(new List<string>() {"RADAR2:Test:N050.57.00.000:W001.21.24.490:1:2:3::::7:8:9;comment"});

            Radar result = sectorElementCollection.Radars[0];
            Assert.Equal("Test", result.Name);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Coordinate);
            Assert.Equal(new RadarParameters(1, 2, 3), result.PrimaryRadarParameters);
            Assert.Equal(new RadarParameters(), result.SModeRadarParameters);
            Assert.Equal(new RadarParameters(7, 8, 9), result.CModeRadarParameters);
            AssertExpectedMetadata(result);
        }
        
        [Fact]
        public void TestItAddsRadarDataCModeMissing()
        {
            RunParserOnLines(new List<string>() {"RADAR2:Test:N050.57.00.000:W001.21.24.490:1:2:3:4:5:6:::;comment"});

            Radar result = sectorElementCollection.Radars[0];
            Assert.Equal("Test", result.Name);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Coordinate);
            Assert.Equal(new RadarParameters(1, 2, 3), result.PrimaryRadarParameters);
            Assert.Equal(new RadarParameters(4, 5, 6), result.SModeRadarParameters);
            Assert.Equal(new RadarParameters(), result.CModeRadarParameters);
            AssertExpectedMetadata(result);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.ESE_RADAR2;
        }
    }
}
