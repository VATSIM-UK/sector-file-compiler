using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Input;
using Compiler.Model;

namespace CompilerTest.Parser
{
    public class GeoParserTest: AbstractParserTestCase
    {
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "TestGeo                     N050.57.00.000 W001.21.24.490 N050.57.00.000 W001.21.24.490 test test"
            }}, // Too many sections
            new object[] { new List<string>{
                "N050.57.00.000 W001.21.24.490 N050.57.00.000 W001.21.24.490"
            }}, // Too few sections
            new object[] { new List<string>{
                "TestGeo                     N050.57.00.000 N050.57.00.001 N050.57.00.000 W001.21.24.490 test"
            }}, // First point invalid
            new object[] { new List<string>{
                "TestGeo                     N050.57.00.000 W001.21.24.490 N050.57.00.000 N050.57.00.001 test"
            }}, // Second point invalid
        };

        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            this.RunParserOnLines(lines);
            Assert.Empty(this.sectorElementCollection.GeoElements);
            this.logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsGeoDataWithOneSegment()
        {
            this.RunParserOnLines(
                new List<string>(new[] { "TestGeo                     N050.57.00.000 W001.21.24.490 BCN BCN test ;comment" })
            );

            Geo result = this.sectorElementCollection.GeoElements[0];
            Assert.Equal(
                new Point(new Coordinate("N050.57.00.000", "W001.21.24.490")),
                result.FirstPoint
            );
            Assert.Equal(
                new Point("BCN"),
                result.SecondPoint
            );
            Assert.Equal(
                "test",
                result.Colour
            );
            Assert.Empty(result.AdditionalSegments);
            this.AssertExpectedMetadata(result);
        }
        
        [Fact]
        public void TestItAddsGeoDataWithMultipleSegment()
        {
            this.RunParserOnLines(
                new List<string>(new[]
                {
                    "TestGeo                     N050.57.00.000 W001.21.24.490 BCN BCN test ;comment",
                    "                            N051.57.00.000 W002.21.24.490 BHD BHD test2 ;comment1",
                    "                            N053.57.00.000 W003.21.24.490 LAM LAM test3 ;comment2"
                })
            );

            Geo result = this.sectorElementCollection.GeoElements[0];
            Assert.Equal(
                new Point(new Coordinate("N050.57.00.000", "W001.21.24.490")),
                result.FirstPoint
            );
            Assert.Equal(
                new Point("BCN"),
                result.SecondPoint
            );
            Assert.Equal(
                "test",
                result.Colour
            );
            this.AssertExpectedMetadata(result);
            
            // Segment 1
            Assert.Equal(2, result.AdditionalSegments.Count);
            Assert.Equal(
                new Point(new Coordinate("N051.57.00.000", "W002.21.24.490")),
                result.AdditionalSegments[0].FirstPoint
            );
            Assert.Equal(
                new Point("BHD"),
                result.AdditionalSegments[0].SecondPoint
            );
            Assert.Equal(
                "test2",
                result.AdditionalSegments[0].Colour
            );
            this.AssertExpectedMetadata(result.AdditionalSegments[0], 2, "comment1");
            
            // Segment 2
            Assert.Equal(
                new Point(new Coordinate("N053.57.00.000", "W003.21.24.490")),
                result.AdditionalSegments[1].FirstPoint
            );
            Assert.Equal(
                new Point("LAM"),
                result.AdditionalSegments[1].SecondPoint
            );
            Assert.Equal(
                "test3",
                result.AdditionalSegments[1].Colour
            );
            this.AssertExpectedMetadata(result.AdditionalSegments[1], 3, "comment2");
        }

        [Fact]
        public void TestItAddsFakePoint()
        {
            this.RunParserOnLines(
                new List<string>(new[] { "TestGeo                     S999.00.00.000 E999.00.00.000 S999.00.00.000 E999.00.00.000 ;comment" })
            );
            
            Geo result = this.sectorElementCollection.GeoElements[0];
            Assert.Equal(
                new Point(new Coordinate("S999.00.00.000", "E999.00.00.000")),
                result.FirstPoint
            );
            Assert.Equal(
                new Point(new Coordinate("S999.00.00.000", "E999.00.00.000")),
                result.SecondPoint
            );
            Assert.Null(result.Colour);
            this.AssertExpectedMetadata(result);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.SCT_GEO;
        }
    }
}
