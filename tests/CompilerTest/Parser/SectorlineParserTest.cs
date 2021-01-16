using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class SectorlineParserTest: AbstractParserTestCase
    {
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "NOPE_SECTORLINE:BBTWR:EGBB:2.5 ;comment",
                "DISPLAY:BBAPP:BBAPP:BBTWR ;comment1",
                "DISPLAY:BBTWR:BBAPP:BBTWR ;comment2",
            }}, // Invalid type
            new object[] { new List<string>{
                "CIRCLE_SECTORLINE:BBTWR:EGBB ;comment",
                "DISPLAY:BBAPP:BBAPP:BBTWR ;comment1",
                "DISPLAY:BBTWR:BBAPP:BBTWR ;comment2",
            }}, // Invalid segments CIRCLE_SECTORLINE
            new object[] { new List<string>{
                "CIRCLE_SECTORLINE:AEAPP:W054.39.27.000:W006.12.57.000:30 ;comment3",
                "DISPLAY:AEAPP:AEAPP:Rathlin West ;comment4"
            }}, // Invalid coordinate CIRCLE_SECTORLINE
            new object[] { new List<string>{
                "CIRCLE_SECTORLINE:BBTWR:EGBB:abc ;comment",
                "DISPLAY:BBAPP:BBAPP:BBTWR ;comment1",
                "DISPLAY:BBTWR:BBAPP:BBTWR ;comment2",
            }}, // Invalid radius CIRCLE_SECTORLINE
            new object[] { new List<string>{
                "CIRCLE_SECTORLINE:BBTWR:EGBB:2.5 ;comment",
                "DISPLAY:BBAPP:BBAPP ;comment1",
                "DISPLAY:BBTWR:BBAPP:BBTWR ;comment2",
            }}, // Invalid display rule CIRCLE_SECTORLINE
            new object[] { new List<string>{
                "SECTORLINE:JJCTR - S6:LOLOL ;comment1",
                "DISPLAY:London S6:JJCTR:London S6 ;comment2",
                "DISPLAY:JJCTR:JJCTR:London S6 ;comment3",
                "COORD:N050.00.00.000:W002.40.34.000 ;comment4",
                "COORD:N049.59.60.000:W002.29.35.000 ;comment5",
            }}, // Invalid declaration SECTORLINE
            new object[] { new List<string>{
                "SECTORLINE:JJCTR - S6 ;comment1",
                "WHATDISPLAY:London S6:JJCTR:London S6 ;comment2",
                "DISPLAY:JJCTR:JJCTR:London S6 ;comment3",
                "COORD:N050.00.00.000:W002.40.34.000 ;comment4",
                "COORD:N049.59.60.000:W002.29.35.000 ;comment5",
            }}, // Unknown row SECTORLINE
            new object[] { new List<string>{
                "SECTORLINE:JJCTR - S6 ;comment1",
                "DISPLAY:London S6:JJCTR:London S6 ;comment2",
                "DISPLAY:JJCTR:JJCTR:London S6 ;comment3",
            }}, // No coordinates SECTORLINE
            new object[] { new List<string>{
                "SECTORLINE:JJCTR - S6 ;comment1",
                "DISPLAY:London S6:JJCTR:London S6:HI ;comment2",
                "DISPLAY:JJCTR:JJCTR:London S6 ;comment3",
                "COORD:N050.00.00.000:W002.40.34.000 ;comment4",
                "COORD:N049.59.60.000:W002.29.35.000 ;comment5",
            }}, // Invalid display rule SECTORLINE
            new object[] { new List<string>{
                "SECTORLINE:JJCTR - S6 ;comment1",
                "DISPLAY:London S6:JJCTR:London S6 ;comment2",
                "DISPLAY:JJCTR:JJCTR:London S6 ;comment3",
                "COORD:N050.00.00.000:W002.40.34.000:HI ;comment4",
                "COORD:N049.59.60.000:W002.29.35.000 ;comment5",
            }}, // Too many coordinate segments SECTORLINE
            new object[] { new List<string>{
                "SECTORLINE:JJCTR - S6 ;comment1",
                "DISPLAY:London S6:JJCTR:London S6 ;comment2",
                "DISPLAY:JJCTR:JJCTR:London S6 ;comment3",
                "COORD:W050.00.00.000:W002.40.34.000 ;comment4",
                "COORD:N049.59.60.000:W002.29.35.000 ;comment5",
            }}, // Invalid coordinate SECTORLINE
        };

        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            this.RunParserOnLines(lines);
            Assert.Empty(this.sectorElementCollection.SectorLines);
            Assert.Empty(this.sectorElementCollection.CircleSectorLines);
            this.logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsCircleSectorlines()
        {
            this.RunParserOnLines(
                new List<string>(new string[] {
                    "CIRCLE_SECTORLINE:BBTWR:EGBB:2.5 ;comment",
                    "DISPLAY:BBAPP:BBAPP:BBTWR ;comment1",
                    "DISPLAY:BBTWR:BBAPP:BBTWR ;comment2",
                    "CIRCLE_SECTORLINE:AEAPP:N054.39.27.000:W006.12.57.000:30 ;comment3",
                    "DISPLAY:AEAPP:AEAPP:Rathlin West ;comment4"
                })    
            );

            // First
            CircleSectorline result1 = this.sectorElementCollection.CircleSectorLines[0];
            Assert.Equal("BBTWR", result1.Name);
            Assert.Equal("EGBB", result1.CentrePoint);
            Assert.Equal(2.5, result1.Radius);
            this.AssertExpectedMetadata(result1, 1, "comment");

            Assert.Equal(2, result1.DisplayRules.Count);
            Assert.Equal("BBAPP", result1.DisplayRules[0].ControlledSector);
            Assert.Equal("BBAPP", result1.DisplayRules[0].CompareSectorFirst);
            Assert.Equal("BBTWR", result1.DisplayRules[0].CompareSectorSecond);
            this.AssertExpectedMetadata(result1.DisplayRules[0], 2, "comment1");
            
            Assert.Equal("BBTWR", result1.DisplayRules[1].ControlledSector);
            Assert.Equal("BBAPP", result1.DisplayRules[1].CompareSectorFirst);
            Assert.Equal("BBTWR", result1.DisplayRules[1].CompareSectorSecond);
            this.AssertExpectedMetadata(result1.DisplayRules[1], 3, "comment2");

            // Second
            CircleSectorline result2 = this.sectorElementCollection.CircleSectorLines[1];
            Assert.Equal("AEAPP", result2.Name);
            Assert.Equal(new Coordinate("N054.39.27.000", "W006.12.57.000"), result2.CentreCoordinate);
            Assert.Equal(30, result2.Radius);
            this.AssertExpectedMetadata(result2, 4, "comment3");

            Assert.Single(result2.DisplayRules);
            Assert.Equal("AEAPP", result2.DisplayRules[0].ControlledSector);
            Assert.Equal("AEAPP", result2.DisplayRules[0].CompareSectorFirst);
            Assert.Equal("Rathlin West", result2.DisplayRules[0].CompareSectorSecond);
            this.AssertExpectedMetadata(result2.DisplayRules[0], 5, "comment4");
        }

        [Fact]
        public void TestItAddsSectorlines()
        {
            this.RunParserOnLines(
                new List<string>(new string[] {
                    "SECTORLINE:JJCTR - S6 ;comment1",
                    "DISPLAY:London S6:JJCTR:London S6 ;comment2",
                    "DISPLAY:JJCTR:JJCTR:London S6 ;comment3",
                    "COORD:N050.00.00.000:W002.40.34.000 ;comment4",
                    "COORD:N049.59.59.000:W002.29.35.000 ;comment5",
                    "SECTORLINE:JJCTR - LS ;comment6",
                    "DISPLAY:London AC Worthing:JJCTR:London AC Worthing ;comment7",
                    "DISPLAY:JJCTR:JJCTR:London AC Worthing ;comment8",
                    "COORD:N049.59.59.000:W002.29.35.000 ;comment9",
                    "COORD:N050.00.00.000:W001.47.00.000 ;comment10",
                })
            );

            // First
            Sectorline result1 = this.sectorElementCollection.SectorLines[0];
            Assert.Equal("JJCTR - S6", result1.Name);
            this.AssertExpectedMetadata(result1, 1, "comment1");
            
            Assert.Equal(2, result1.DisplayRules.Count);
            Assert.Equal("London S6", result1.DisplayRules[0].ControlledSector);
            Assert.Equal("JJCTR", result1.DisplayRules[0].CompareSectorFirst);
            Assert.Equal("London S6", result1.DisplayRules[0].CompareSectorSecond);
            this.AssertExpectedMetadata(result1.DisplayRules[0], 2, "comment2");
            
            Assert.Equal("JJCTR", result1.DisplayRules[1].ControlledSector);
            Assert.Equal("JJCTR", result1.DisplayRules[1].CompareSectorFirst);
            Assert.Equal("London S6", result1.DisplayRules[1].CompareSectorSecond);
            this.AssertExpectedMetadata(result1.DisplayRules[1], 3, "comment3");

            Assert.Equal(2, result1.Coordinates.Count);
            Assert.Equal(new Coordinate("N050.00.00.000", "W002.40.34.000"), result1.Coordinates[0].Coordinate);
            this.AssertExpectedMetadata(result1.Coordinates[0], 4, "comment4");
            
            Assert.Equal(new Coordinate("N049.59.59.000", "W002.29.35.000"), result1.Coordinates[1].Coordinate);
            this.AssertExpectedMetadata(result1.Coordinates[1], 5, "comment5");

            // Second
            Sectorline result2 = this.sectorElementCollection.SectorLines[1];
            Assert.Equal("JJCTR - LS", result2.Name);
            this.AssertExpectedMetadata(result2, 6, "comment6");

            
            Assert.Equal(2, result2.DisplayRules.Count);
            Assert.Equal("London AC Worthing", result2.DisplayRules[0].ControlledSector);
            Assert.Equal("JJCTR", result2.DisplayRules[0].CompareSectorFirst);
            Assert.Equal("London AC Worthing", result2.DisplayRules[0].CompareSectorSecond);
            this.AssertExpectedMetadata(result2.DisplayRules[0], 7, "comment7");
            
            Assert.Equal("JJCTR", result2.DisplayRules[1].ControlledSector);
            Assert.Equal("JJCTR", result2.DisplayRules[1].CompareSectorFirst);
            Assert.Equal("London AC Worthing", result2.DisplayRules[1].CompareSectorSecond);
            this.AssertExpectedMetadata(result2.DisplayRules[1], 8, "comment8");

            
            Assert.Equal(2, result2.Coordinates.Count);
            Assert.Equal(new Coordinate("N049.59.59.000", "W002.29.35.000"), result2.Coordinates[0].Coordinate);
            this.AssertExpectedMetadata(result2.Coordinates[0], 9, "comment9");
            
            Assert.Equal(new Coordinate("N050.00.00.000", "W001.47.00.000"), result2.Coordinates[1].Coordinate);
            this.AssertExpectedMetadata(result2.Coordinates[1], 10, "comment10");
        }

        [Fact]
        public void TestItAddsMixedData()
        {
            this.RunParserOnLines(
                new List<string>(new string[] {
                    "SECTORLINE:JJCTR - LS ;comment6",
                    "DISPLAY:London AC Worthing:JJCTR:JJCTR ;comment7",
                    "DISPLAY:JJCTR:JJCTR:London AC Worthing ;comment8",
                    "COORD:N049.59.59.000:W002.29.35.000 ;comment9",
                    "COORD:N050.00.00.000:W001.47.00.000 ;comment10",
                    "",
                    "CIRCLE_SECTORLINE:BBTWR:EGBB:2.5 ;comment",
                    "DISPLAY:BBAPP:BBAPP:BBTWR ;comment1",
                    "DISPLAY:BBTWR:BBAPP:BBTWR ;comment2",
                })
            );

            // First
            CircleSectorline result1 = this.sectorElementCollection.CircleSectorLines[0];
            Assert.Equal("BBTWR", result1.Name);
            Assert.Equal("EGBB", result1.CentrePoint);
            Assert.Equal(2.5, result1.Radius);
            this.AssertExpectedMetadata(result1, 7);

            Assert.Equal(2, result1.DisplayRules.Count);
            Assert.Equal("BBAPP", result1.DisplayRules[0].ControlledSector);
            Assert.Equal("BBAPP", result1.DisplayRules[0].CompareSectorFirst);
            Assert.Equal("BBTWR", result1.DisplayRules[0].CompareSectorSecond);
            this.AssertExpectedMetadata(result1.DisplayRules[0], 8, "comment1");
            
            Assert.Equal("BBTWR", result1.DisplayRules[1].ControlledSector);
            Assert.Equal("BBAPP", result1.DisplayRules[1].CompareSectorFirst);
            Assert.Equal("BBTWR", result1.DisplayRules[1].CompareSectorSecond);
            this.AssertExpectedMetadata(result1.DisplayRules[1], 9, "comment2");


            // Second
            Sectorline result2 = this.sectorElementCollection.SectorLines[0];
            Assert.Equal("JJCTR - LS", result2.Name);
            this.AssertExpectedMetadata(result2, 1, "comment6");

            
            Assert.Equal(2, result2.DisplayRules.Count);
            Assert.Equal("London AC Worthing", result2.DisplayRules[0].ControlledSector);
            Assert.Equal("JJCTR", result2.DisplayRules[0].CompareSectorFirst);
            Assert.Equal("JJCTR", result2.DisplayRules[0].CompareSectorSecond);
            this.AssertExpectedMetadata(result2.DisplayRules[0], 2, "comment7");
            
            Assert.Equal("JJCTR", result2.DisplayRules[1].ControlledSector);
            Assert.Equal("JJCTR", result2.DisplayRules[1].CompareSectorFirst);
            Assert.Equal("London AC Worthing", result2.DisplayRules[1].CompareSectorSecond);
            this.AssertExpectedMetadata(result2.DisplayRules[1], 3, "comment8");

            
            Assert.Equal(2, result2.Coordinates.Count);
            Assert.Equal(new Coordinate("N049.59.59.000", "W002.29.35.000"), result2.Coordinates[0].Coordinate);
            this.AssertExpectedMetadata(result2.Coordinates[0], 4, "comment9");
            
            Assert.Equal(new Coordinate("N050.00.00.000", "W001.47.00.000"), result2.Coordinates[1].Coordinate);
            this.AssertExpectedMetadata(result2.Coordinates[1], 5, "comment10");
        }

        [Fact]
        public void TestItAddsMixedDataNoDisplayRules()
        {
            this.RunParserOnLines(
                new List<string>(new string[] {
                    "SECTORLINE:JJCTR - LS ;comment6",
                    "COORD:N049.59.59.000:W002.29.35.000 ;comment9",
                    "COORD:N050.00.00.000:W001.47.00.000 ;comment10",
                    "",
                    "CIRCLE_SECTORLINE:BBTWR:EGBB:2.5 ;comment",
                })
            );

            // First
            CircleSectorline result1 = this.sectorElementCollection.CircleSectorLines[0];
            Assert.Equal("BBTWR", result1.Name);
            Assert.Equal("EGBB", result1.CentrePoint);
            Assert.Equal(2.5, result1.Radius);
            Assert.Empty(result1.DisplayRules);


            // Second
            Sectorline result2 = this.sectorElementCollection.SectorLines[0];
            Assert.Equal("JJCTR - LS", result2.Name);
            Assert.Empty(result2.DisplayRules);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.ESE_SECTORLINES;
        }
    }
}
