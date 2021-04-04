using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class RegionParserTest : AbstractParserTestCase
    {
        [Fact]
        public void TestItAddsSinglePointRegionData()
        {
            RunParserOnLines(
                new List<string>(new[]
                {
                    "REGIONNAME TestRegion",
                    "Red BCN BCN ;comment",
                })
            );

            Region result = sectorElementCollection.Regions[0];
            Assert.Equal("TestRegion", result.Name);
            AssertExpectedMetadata(result, commentString: "");
            
            Assert.Single(result.Points);
            Assert.Equal("Red", result.Points[0].Colour);
            Assert.Equal(new Point("BCN"), result.Points[0].Point);
            AssertExpectedMetadata(result.Points[0], 2);
        }

        [Fact]
        public void TestItAddsMultipleLineRegionData()
        {
            RunParserOnLines(
                new List<string>(new[]
                {
                    "REGIONNAME TestRegion ; comment",
                    "Red BCN BCN ;comment",
                    "BHD BHD",
                    " JSY JSY"
                })
            );

            Region result = sectorElementCollection.Regions[0];
            Assert.Equal("TestRegion", result.Name);
            AssertExpectedMetadata(result);
            Assert.Equal(3, result.Points.Count);
            
            Assert.Equal("Red", result.Points[0].Colour);
            Assert.Equal(new Point("BCN"), result.Points[0].Point);
            AssertExpectedMetadata(result.Points[0], 2);
            
            Assert.Null(result.Points[1].Colour);
            Assert.Equal(new Point("BHD"), result.Points[1].Point);
            AssertExpectedMetadata(result.Points[1], 3, "");
            
            Assert.Null(result.Points[2].Colour);
            Assert.Equal(new Point("JSY"), result.Points[2].Point);
            AssertExpectedMetadata(result.Points[2], 4, "");
        }

        [Fact]
        public void TestItAddsMultipleRegionsData()
        {
            RunParserOnLines(
                new List<string>(new[]
                {
                    "REGIONNAME TestRegion1",
                    "Red BCN BCN ;comment",
                    " BHD BHD",
                    "REGIONNAME TestRegion2",
                    "White JSY JSY"
                })
            );
            
            Assert.Equal(2, sectorElementCollection.Regions.Count);
            Region result1 = sectorElementCollection.Regions[0];
            Assert.Equal("TestRegion1", result1.Name);
            AssertExpectedMetadata(result1, 1, "");

            Assert.Equal(2, result1.Points.Count);
            Assert.Equal("Red", result1.Points[0].Colour);
            Assert.Equal(new Point("BCN"), result1.Points[0].Point);
            AssertExpectedMetadata(result1.Points[0], 2);
            
            Assert.Equal(new Point("BHD"), result1.Points[1].Point);
            AssertExpectedMetadata(result1.Points[1], 3, "");

            Region result2 = sectorElementCollection.Regions[1];
            Assert.Equal("TestRegion2", result2.Name);
            AssertExpectedMetadata(result2, 4, "");
            
            Assert.Single(result2.Points);
            Assert.Equal(new Point("JSY"), result2.Points[0].Point);
            Assert.Equal("White", result2.Points[0].Colour);
            AssertExpectedMetadata(result2.Points[0], 5, "");
        }

        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[]
            {
                new List<string>
                {
                    "BCN BCN ;comment", " BHD BHD"
                }
            }, // Invalid first line
            new object[]
            {
                new List<string>
                {
                    "REGIONNAME TestRegion", "Red BCN BHD ;comment",
                    " BHD BHD"
                }
            }, // Invalid first region point
            new object[]
            {
                new List<string>
                {
                    "REGIONNAME TestRegion", "Red BCN BCN ;comment",
                    "BHD MID"
                }
            }, // Invalid continuation point
            new object[]
            {
                new List<string>
                {
                    "REGIONNAME TestRegion",
                    "Red BCN BCN ;comment",
                    "Red BCN BCN ;comment"
                }
            }, // Unexpected colour
            new object[]
            {
                new List<string>
                {
                    "REGIONNAME TestRegion",
                    "BCN BCN ;comment"
                }
            }, // No colour
            new object[]
            {
                new List<string>
                {
                    "REGIONNAME TestRegion2"
                }
            }, // Incomplete region at end of file
            new object[]
            {
                new List<string>
                {
                    "REGIONNAME TestRegion1",
                    "REGIONNAME TestRegion2",
                    "White JSY JSY"
                }
            }, // Incomplete mid-file
            new object[]
            {
                new List<string>
                {
                    "REGIONNAME TestRegion",
                    "Red BCN BCN ;comment",
                    "A"
                }
            },  // Not enough points data
            new object[]
            {
                new List<string>
                {
                    "REGIONNAME TestRegion",
                    "Red BCN BCN ;comment",
                    "A B C"
                }
            }  // Too much points data
        };

        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            RunParserOnLines(lines);

            Assert.Empty(sectorElementCollection.Regions);
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.SCT_REGIONS;
        }
    }
}
