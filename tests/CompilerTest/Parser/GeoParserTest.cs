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
            new object[] { new List<string>{
                "COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:-5:*:|HEMEL20 ;comment"
            }}, // Climb level negative
            new object[] { new List<string>{
                "COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:*:abc:|HEMEL20 ;comment"
            }}, // Descend level not integer
            new object[] { new List<string>{
                "COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:*:-5:|HEMEL20 ;comment"
            }}, // Descend level negative
            new object[] { new List<string>{
                "FIR_COPX:*:*:HEMEL:*:26R:London AC Worthing:London AC Dover:*:25000:|HEMEL20 ;comment"
            }}, // Arrival airport any, but runway set
            new object[] { new List<string>{
                "FIR_COPX:*:*:HEMEL:DIKAS:26R:London AC Worthing:London AC Dover:*:25000:|HEMEL20 ;comment"
            }}, // Next fix is a fix (not an airport) but arrival runway is specified
            new object[] { new List<string>{
                "FIR_COPX:*:09R:HEMEL:EGKK:26R:London AC Worthing:London AC Dover:*:25000:|HEMEL20 ;comment"
            }}, // Any departure airport, but runway specified
            new object[] { new List<string>{
                "FIR_COPX:IBROD:09R:HEMEL:EGKK:26R:London AC Worthing:London AC Dover:*:25000:|HEMEL20 ;comment"
            }}, // Next fix is a fix (not an airport) but departure runway is specified
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
                new List<string>(new string[] { "TestGeo                     N050.57.00.000 W001.21.24.490 BCN BCN test ;comment" })
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
            this.AssertExpectedMetadata(result, 1, "comment");
        }
        
        [Fact]
        public void TestItAddsGeoDataWithMultipleSegment()
        {
            this.RunParserOnLines(
                new List<string>(new string[]
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
            this.AssertExpectedMetadata(result, 1, "comment");
            
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
            this.AssertExpectedMetadata(result.AdditionalSegments[1], 2, "comment2");
        }

        [Fact]
        public void TestItAddsFakePoint()
        {
            this.RunParserOnLines(
                new List<string>(new string[] { "TestGeo                     S999.00.00.000 E999.00.00.000 S999.00.00.000 E999.00.00.000" })
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
            Assert.Equal(
                "0",
                result.Colour
            );
            this.AssertExpectedMetadata(result, 1, "comment");
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.SCT_GEO;
        }
    }
}
