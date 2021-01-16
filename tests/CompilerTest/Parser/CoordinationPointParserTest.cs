using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class CoordinationPointParserTest: AbstractParserTestCase
    {
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "COPX:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:*:25000:|HEMEL20 ;comment"
            }}, // Incorrect number of segments
            new object[] { new List<string>{
                "COPN:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:*:25000:|HEMEL20 ;comment"
            }}, // Type unknown
            new object[] { new List<string>{
                "COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:25000:25000:|HEMEL20 ;comment"
            }}, // Both climb and descend set
            new object[] { new List<string>{
                "COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:abc:25000:|HEMEL20 ;comment"
            }}, // Climb level not integer
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

            Assert.Empty(this.sectorElementCollection.CoordinationPoints);
            this.logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
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
            this.RunParserOnLines(new List<string> {line});

            CoordinationPoint result = this.sectorElementCollection.CoordinationPoints[0];
            Assert.False(result.IsFirCopx);
            Assert.Equal(CoordinationPoint.DataNotSpecified, result.DepartureAirportOrFixBefore);
            Assert.Equal(CoordinationPoint.DataNotSpecified, result.DepartureRunway);
            Assert.Equal("HEMEL", result.CoordinationFix);
            Assert.Equal("EGBB", result.ArrivalAirportOrFixAfter);
            Assert.Equal(CoordinationPoint.DataNotSpecified, result.ArrivalRunway);
            Assert.Equal("London AC Worthing", result.FromSector);
            Assert.Equal("London AC Dover", result.ToSector);
            Assert.Equal(climbLevel, result.ClimbLevel);
            Assert.Equal(descendLevel, result.DescendLevel);
            Assert.Equal("|HEMEL20", result.Name);
            this.AssertExpectedMetadata(result);
        }

        [Fact]
        public void TestItAddsFirCoordinationPoints()
        {
            this.RunParserOnLines(
                new List<string>(new[] { "FIR_COPX:*:*:HEMEL:EGBB:*:London AC Worthing:London AC Dover:*:25000:|HEMEL20 ;comment" })
            );
            
            CoordinationPoint result = this.sectorElementCollection.CoordinationPoints[0];
            Assert.True(result.IsFirCopx);
            Assert.Equal(CoordinationPoint.DataNotSpecified, result.DepartureAirportOrFixBefore);
            Assert.Equal(CoordinationPoint.DataNotSpecified, result.DepartureRunway);
            Assert.Equal("HEMEL", result.CoordinationFix);
            Assert.Equal("EGBB", result.ArrivalAirportOrFixAfter);
            Assert.Equal(CoordinationPoint.DataNotSpecified, result.ArrivalRunway);
            Assert.Equal("London AC Worthing", result.FromSector);
            Assert.Equal("London AC Dover", result.ToSector);
            Assert.Equal(CoordinationPoint.DataNotSpecified, result.ClimbLevel);
            Assert.Equal("25000", result.DescendLevel);
            Assert.Equal("|HEMEL20", result.Name);
            this.AssertExpectedMetadata(result);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.ESE_AGREEMENTS;
        }
    }
}
