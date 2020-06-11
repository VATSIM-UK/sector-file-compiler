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
    public class EsePositionParserTest
    {
        private readonly EsePositionParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public EsePositionParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (EsePositionParser)(new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.ESE_POSITIONS);
        }

        [Fact]
        public void TestItAddsPositionData()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:W002.43.29.830:N051.32.26.871:W002.43.29.831:N051.32.26.872:W002.43.29.832:N051.32.26.873:W002.43.29.833 ;comment" })
            );

            this.parser.ParseData(data);

            List<Coordinate> coordinateList = new List<Coordinate>();
            coordinateList.Add(new Coordinate("N051.32.26.870", "W002.43.29.830"));
            coordinateList.Add(new Coordinate("N051.32.26.871", "W002.43.29.831"));
            coordinateList.Add(new Coordinate("N051.32.26.872", "W002.43.29.832"));
            coordinateList.Add(new Coordinate("N051.32.26.873", "W002.43.29.833"));
            ControllerPosition position = this.collection.EsePositions[0];
            Assert.Equal("LON_CTR", position.Callsign);
            Assert.Equal("London Control", position.RtfCallsign);
            Assert.Equal("127.820", position.Frequency);
            Assert.Equal("L", position.Identifier);
            Assert.Equal("9", position.MiddleLetter);
            Assert.Equal("LON", position.Prefix);
            Assert.Equal("CTR", position.Suffix);
            Assert.Equal("0301", position.SquawkRangeStart);
            Assert.Equal("0377", position.SquawkRangeEnd);
            Assert.Equal(coordinateList, position.VisCentres);
        }

        [Fact]
        public void TestItAddsPositionDataSkippedCenters()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:W002.43.29.830:::::: ;comment" })
            );

            this.parser.ParseData(data);

            List<Coordinate> coordinateList = new List<Coordinate>();
            coordinateList.Add(new Coordinate("N051.32.26.870", "W002.43.29.830"));
            ControllerPosition position = this.collection.EsePositions[0];
            Assert.Equal("LON_CTR", position.Callsign);
            Assert.Equal("London Control", position.RtfCallsign);
            Assert.Equal("127.820", position.Frequency);
            Assert.Equal("L", position.Identifier);
            Assert.Equal("9", position.MiddleLetter);
            Assert.Equal("LON", position.Prefix);
            Assert.Equal("CTR", position.Suffix);
            Assert.Equal("0301", position.SquawkRangeStart);
            Assert.Equal("0377", position.SquawkRangeEnd);
            Assert.Equal(coordinateList, position.VisCentres);
        }

        [Fact]
        public void TestItAddsPositionDataSkippedSquawks()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-::-:N051.32.26.870:W002.43.29.830 ;comment" })
            );

            this.parser.ParseData(data);

            List<Coordinate> coordinateList = new List<Coordinate>();
            coordinateList.Add(new Coordinate("N051.32.26.870", "W002.43.29.830"));
            ControllerPosition position = this.collection.EsePositions[0];
            Assert.Equal("LON_CTR", position.Callsign);
            Assert.Equal("London Control", position.RtfCallsign);
            Assert.Equal("127.820", position.Frequency);
            Assert.Equal("L", position.Identifier);
            Assert.Equal("9", position.MiddleLetter);
            Assert.Equal("LON", position.Prefix);
            Assert.Equal("CTR", position.Suffix);
            Assert.Equal("", position.SquawkRangeStart);
            Assert.Equal("-", position.SquawkRangeEnd);
            Assert.Equal(coordinateList, position.VisCentres);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorInvalidNumberOfSegments()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorInvalidRtfFrequency()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "LON_CTR:London Control:127.821:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:W002.43.29.830 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorInvalidSuffix()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "LON_CTR:London Control:127.820:L:9:LON:LOL:-:-:0301:0377:N051.32.26.870:W002.43.29.830 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorInvalidStartSquawkRange()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0381:0377:N051.32.26.870:W002.43.29.830 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorInvalidEndSquawkRange()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0378:N051.32.26.870:W002.43.29.830 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorSquawkRangeEndLessThanStart()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0300:N051.32.26.870:W002.43.29.830 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorTooManyVisCenters()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:W002.43.29.830:::::::: ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorVisCenterMissingLongitude()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:W002.43.29.830: ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorVisCenterInvalidCoordinate()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:N002.43.29.830 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }
    }
}
