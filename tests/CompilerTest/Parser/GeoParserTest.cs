using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Parser;
using Compiler.Error;
using Compiler.Model;
using Compiler.Event;
using Compiler.Output;

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
            this.parser = (GeoParser)(new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.SCT_GEO);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorTooManySections()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "TestGeo                     N050.57.00.000 W001.21.24.490 N050.57.00.000 W001.21.24.490 test test" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Exactly(2));
        }

        [Fact]
        public void TestItRaisesSyntaxErrorTooFewSections()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "N050.57.00.000 W001.21.24.490 N050.57.00.000 W001.21.24.490" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Exactly(2));
        }

        [Fact]
        public void TestItRaisesSyntaxErrorFirstPointInvalid()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "TestGeo                     N050.57.00.000 N050.57.00.001 N050.57.00.000 W001.21.24.490 test" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Exactly(2));
        }

        [Fact]
        public void TestItRaisesSyntaxErrorSecondPointInvalid()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
                new List<string>(new string[] { "TestGeo                     N050.57.00.000 W001.21.24.490 N050.57.00.000 N050.57.00.001 test" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Exactly(2));
        }

        [Fact]
        public void TestItHandlesMetadata()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "" })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.SCT_GEO][0]);
        }

        [Fact]
        public void TestItAddsGeoData()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
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
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "EGHI",
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
