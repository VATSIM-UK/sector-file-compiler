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
    public class CoordinationPointParserTest
    {
        private readonly AirspaceParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public CoordinationPointParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (AirspaceParser)(new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.ESE_AIRSPACE);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfIncorrectNumberOfSegments()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "COPX:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:*:25000:|HEMEL20 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfTypeUnknown()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "COPN:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:*:25000:|HEMEL20 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfBothClimbAndDescendSet()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:25000:25000:|HEMEL20 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfClimbLevelNotInteger()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:abc:25000:|HEMEL20 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfClimbLevelNegative()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:-5:*:|HEMEL20 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfDescendLevelNotInteger()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:*:abc:|HEMEL20 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfDescendLevelNegative()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:*:-5:|HEMEL20 ;comment" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
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
            Assert.IsType<BlankLine>(
                this.collection.Compilables[OutputSections.ESE_AIRSPACE][0]
            );
        }

        [Theory]
        [
            InlineData(
                "COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:*:25000:|HEMEL20 ;comment",
                "*",
                "25000"
            )
        ] // Climb unspecified, descend specified
        [
            InlineData(
                "COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:25000:*:|HEMEL20 ;comment",
                "25000",
                "*"
            )
        ] // Climb specified, descend unspecified
        [
            InlineData(
                "COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:*:*:|HEMEL20 ;comment",
                "*",
                "*"
            )
        ] // Climb unspecified, descend unspecified
        public void TestItAddsInternalCoordinationPoints(string line, string climbLevel, string descendLevel)
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { line })
            );

            this.parser.ParseData(data);

            CoordinationPoint result = this.collection.CoordinationPoints[0];
            Assert.False(result.IsFirCopx);
            Assert.Equal(CoordinationPoint.DATA_NOT_SPECIFIED, result.DepartureAirportOrFixBefore);
            Assert.Equal(CoordinationPoint.DATA_NOT_SPECIFIED, result.DepartureRunway);
            Assert.Equal("HEMEL", result.CoordinationFix);
            Assert.Equal("EGBB", result.ArrivalAiportOrFixAfter);
            Assert.Equal(CoordinationPoint.DATA_NOT_SPECIFIED, result.ArrivalRunway);
            Assert.Equal("London AC Worthing", result.FromSector);
            Assert.Equal("London AC Dover", result.ToSector);
            Assert.Equal(climbLevel, result.ClimbLevel);
            Assert.Equal(descendLevel, result.DescendLevel);
            Assert.Equal("|HEMEL20", result.Name);
        }

        [Fact]
        public void TestItAddsFirCoordinationPoints()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "FIR_COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:*:25000:|HEMEL20 ;comment" })
            );

            this.parser.ParseData(data);

            CoordinationPoint result = this.collection.CoordinationPoints[0];
            Assert.True(result.IsFirCopx);
            Assert.Equal(CoordinationPoint.DATA_NOT_SPECIFIED, result.DepartureAirportOrFixBefore);
            Assert.Equal(CoordinationPoint.DATA_NOT_SPECIFIED, result.DepartureRunway);
            Assert.Equal("HEMEL", result.CoordinationFix);
            Assert.Equal("EGBB", result.ArrivalAiportOrFixAfter);
            Assert.Equal(CoordinationPoint.DATA_NOT_SPECIFIED, result.ArrivalRunway);
            Assert.Equal("London AC Worthing", result.FromSector);
            Assert.Equal("London AC Dover", result.ToSector);
            Assert.Equal(CoordinationPoint.DATA_NOT_SPECIFIED, result.ClimbLevel);
            Assert.Equal("25000", result.DescendLevel);
            Assert.Equal("|HEMEL20", result.Name);
        }
    }
}
