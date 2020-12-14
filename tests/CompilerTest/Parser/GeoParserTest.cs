using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Parser;
using Compiler.Error;
using Compiler.Model;
using Compiler.Event;
using Compiler.Output;
using CompilerTest.Mock;

namespace CompilerTest.Parser
{
    public class GeoParserTest
    {
        private readonly GeoParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public GeoParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (GeoParser)(new DataParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSectionKeys.SCT_GEO);
        }

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
            this.parser.ParseData(
                new MockSectorDataFile(
                    "test.txt",
                    lines
                )
            );

            Assert.Empty(this.collection.GeoElements);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItHandlesMetadata()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "" })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSectionKeys.SCT_GEO][0]);
        }

        [Fact]
        public void TestItAddsGeoData()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "TestGeo                     N050.57.00.000 W001.21.24.490 BCN BCN test ;comment" })
            );
            this.parser.ParseData(data);

            Geo result = this.collection.GeoElements[0];
            Assert.Equal(
                new Point(new Coordinate("N050.57.00.000", "W001.21.24.490")),
                result.Segments[0].FirstPoint
            );
            Assert.Equal(
                new Point("BCN"),
                result.Segments[0].SecondPoint
            );
            Assert.Equal(
                "test",
                result.Segments[0].Colour
            );
            Assert.Equal(
                "comment",
                result.Segments[0].Comment
            );
        }

        [Fact]
        public void TestItAddsFakePoint()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "TestGeo                     S999.00.00.000 E999.00.00.000 S999.00.00.000 E999.00.00.000" })
            );
            this.parser.ParseData(data);

            Geo result = this.collection.GeoElements[0];
            Assert.Equal(
                new Point(new Coordinate("S999.00.00.000", "E999.00.00.000")),
                result.Segments[0].FirstPoint
            );
            Assert.Equal(
                new Point(new Coordinate("S999.00.00.000", "E999.00.00.000")),
                result.Segments[0].SecondPoint
            );
            Assert.Equal(
                "0",
                result.Segments[0].Colour
            );
            Assert.Equal(
                "Compiler inserted line",
                result.Segments[0].Comment
            );
        }
    }
}
