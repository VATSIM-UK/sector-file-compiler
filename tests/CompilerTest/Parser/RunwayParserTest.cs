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
    public class RunwayParserTest
    {
        private readonly RunwayParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public RunwayParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (RunwayParser)(new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.SCT_RUNWAY);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfTooFewSegments()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "15 33 148 N052.27.48.520 W001.45.31.430 N052.26.46.580 W001.44.22.560 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfInvalidFirstIdentifier()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "37R 33 148 328 N052.27.48.520 W001.45.31.430 N052.26.46.580 W001.44.22.560 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfInvalidReverseIdentifier()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "15 00A 148 328 N052.27.48.520 W001.45.31.430 N052.26.46.580 W001.44.22.560 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfInvalidFirstHeading()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "15 33 abc 328 N052.27.48.520 W001.45.31.430 N052.26.46.580 W001.44.22.560 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfInvalidReverseHeading()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "15 33 148 360 N052.27.48.520 W001.45.31.430 N052.26.46.580 W001.44.22.560 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfInvalidFirstCoordinate()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "15 33 148 328 abc W001.45.31.430 N052.26.46.580 W001.44.22.560 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfInvalidReverseCoordinate()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "15 33 148 328 N052.27.48.520 W001.45.31.430 N052.26.46.580 abc ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }


        [Fact]
        public void TestItAddsRunwayDataNoDescription()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "15 33 148 328 N052.27.48.520 W001.45.31.430 N052.26.46.580 W001.44.22.560 ;comment" })
            );

            this.parser.ParseData(data);

            Runway result = this.collection.Runways[0];
            Assert.Equal("15", result.FirstIdentifier);
            Assert.Equal(148, result.FirstHeading);
            Assert.Equal(new Coordinate("N052.27.48.520", "W001.45.31.430"), result.FirstThreshold);
            Assert.Equal("33", result.ReverseIdentifier);
            Assert.Equal(328, result.ReverseHeading);
            Assert.Equal(new Coordinate("N052.26.46.580", "W001.44.22.560"), result.ReverseThreshold);
            Assert.Equal("", result.RunwayDialogDescription);
            Assert.Equal("comment", result.Comment);
        }

        [Fact]
        public void TestItAddsRunwayDataWithDescription()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "15 33 148 328 N052.27.48.520 W001.45.31.430 N052.26.46.580 W001.44.22.560 EGBB - Birmingham ;comment" })
            );

            this.parser.ParseData(data);

            Runway result = this.collection.Runways[0];
            Assert.Equal("15", result.FirstIdentifier);
            Assert.Equal(148, result.FirstHeading);
            Assert.Equal(new Coordinate("N052.27.48.520", "W001.45.31.430"), result.FirstThreshold);
            Assert.Equal("33", result.ReverseIdentifier);
            Assert.Equal(328, result.ReverseHeading);
            Assert.Equal(new Coordinate("N052.26.46.580", "W001.44.22.560"), result.ReverseThreshold);
            Assert.Equal("EGBB - Birmingham", result.RunwayDialogDescription);
            Assert.Equal("comment", result.Comment);
        }
    }
}
