using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class SectorParserTest : AbstractParserTestCase
    {
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "NOPESECTOR:AAFIN:100:6000 ;comment1",
                "OWNER:AAF:AAR:STA ;comment2",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT:EGAA:EGAC ;comment5",
                "DEPAPT:EGAA ;comment6",
                "ACTIVE:EGLL:09R ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:*:EGAA ;comment7",
            }}, // Invalid declaration
            new object[] { new List<string>{
                "SECTOR:AAFIN:abc:6000 ;comment1",
                "OWNER:AAF:AAR:STA ;comment2",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT:EGAA:EGAC ;comment5",
                "DEPAPT:EGAA ;comment6",
                "ACTIVE:EGLL:09R ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:*:EGAA ;comment7",
            }}, // Invalid lower bound
            new object[] { new List<string>{
                "SECTOR:AAFIN:100:abc ;comment1",
                "OWNER:AAF:AAR:STA ;comment2",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT:EGAA:EGAC ;comment5",
                "DEPAPT:EGAA ;comment6",
                "ACTIVE:EGLL:09R ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:*:EGAA ;comment7",
            }}, // Invalid upper bound
            new object[] { new List<string>{
                "SECTOR:AAFIN:100:6000 ;comment1",
                "OWNER ;comment2",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT:EGAA:EGAC ;comment5",
                "DEPAPT:EGAA ;comment6",
                "ACTIVE:EGLL:09R ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:*:EGAA ;comment7",
            }}, // Invalid owner
            new object[] { new List<string>{
                "SECTOR:AAFIN:100:6000 ;comment1",
                "OWNER:AAF:AAR:STA ;comment2",
                "ALTOWNER:AAWHAT ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT:EGAA:EGAC ;comment5",
                "DEPAPT:EGAA ;comment6",
                "ACTIVE:EGLL:09R ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:*:EGAA ;comment7",
            }}, // Invalid ALTOWNER
            new object[] { new List<string>{
                "SECTOR:AAFIN:100:6000 ;comment1",
                "OWNER:AAF:AAR:STA ;comment2",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER ;comment4",
                "ARRAPT:EGAA:EGAC ;comment5",
                "DEPAPT:EGAA ;comment6",
                "ACTIVE:EGLL:09R ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:*:EGAA ;comment7",
            }}, // Invalid BORDER
            new object[] { new List<string>{
                "SECTOR:AAFIN:100:6000 ;comment1",
                "OWNER:AAF:AAR:STA ;comment2",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT:EGAA:EGAC ;comment5",
                "DEPAPT:EGAA ;comment6",
                "ACTIVE:EGLL ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:*:EGAA ;comment7",
            }}, // Invalid ACTIVE segments
            new object[] { new List<string>{
                "SECTOR:AAFIN:100:6000 ;comment1",
                "OWNER:AAF:AAR:STA ;comment2",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT:EGAA:EGAC ;comment5",
                "DEPAPT:EGAA ;comment6",
                "ACTIVE:LHR:09R ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:*:EGAA ;comment7",
            }}, // Invalid ACTIVE airport
            new object[] { new List<string>{
                "SECTOR:AAFIN:100:6000 ;comment1",
                "OWNER:AAF:AAR:STA ;comment2",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT:EGAA:EGAC ;comment5",
                "DEPAPT:EGAA ;comment6",
                "ACTIVE:EGLL:09R ;comment6.5",
                "ACTIVE:EGLL:36B ;comment6.5.1",
                "GUEST:GDR:*:EGAA ;comment7",
            }}, // Invalid ACTIVE runway
            new object[] { new List<string>{
                "SECTOR:AAFIN:100:6000 ;comment1",
                "OWNER:AAF:AAR:STA ;comment2",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT:EGAA:EGAC ;comment5",
                "DEPAPT:EGAA ;comment6",
                "ACTIVE:EGLL:09R ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:* ;comment7",
            }}, // Invalid number of GUEST segments
            new object[] { new List<string>{
                "SECTOR:AAFIN:100:6000 ;comment1",
                "OWNER:AAF:AAR:STA ;comment2",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT:EGAA:EGAC ;comment5",
                "DEPAPT:EGAA ;comment6",
                "ACTIVE:EGLL:09R ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:*:000A ;comment7",
            }}, // Invalid GUEST arrival airport
            new object[] { new List<string>{
                "SECTOR:AAFIN:100:6000 ;comment1",
                "OWNER:AAF:AAR:STA ;comment2",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT:EGAA:EGAC ;comment5",
                "DEPAPT:EGAA ;comment6",
                "ACTIVE:EGLL:09R ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:LHR:EGAA ;comment7",
            }}, // Invalid GUEST departure airport
            new object[] { new List<string>{
                "SECTOR:AAFIN:100:6000 ;comment1",
                "OWNER:AAF:AAR:STA ;comment2",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT:EGAA:EGAC ;comment5",
                "DEPAPT ;comment6",
                "ACTIVE:EGLL:09R ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:*:EGAA ;comment7",
            }}, // Invalid DEPAPT segments
             new object[] { new List<string>{
                "SECTOR:AAFIN:100:6000 ;comment1",
                "OWNER:AAF:AAR:STA ;comment2",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT:EGAA:EGAC ;comment5",
                "DEPAPT:LHR ;comment6",
                "ACTIVE:EGLL:09R ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:*:EGAA ;comment7",
            }}, // Invalid DEPAPT code
             new object[] { new List<string>{
                "SECTOR:AAFIN:100:6000 ;comment1",
                "OWNER:AAF:AAR:STA ;comment2",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT ;comment5",
                "DEPAPT:EGAA ;comment6",
                "ACTIVE:EGLL:09R ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:*:EGAA ;comment7",
            }}, // Invalid number of ARRAPT segments
            new object[] { new List<string>{
                "SECTOR:AAFIN:100:6000 ;comment1",
                "OWNER:AAF:AAR:STA ;comment2",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT:LHR ;comment5",
                "DEPAPT:EGAA ;comment6",
                "ACTIVE:EGLL:09R ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:*:EGAA ;comment7",
            }}, // Invalid ARRAPT code
            new object[] { new List<string>{
                "SECTOR:AAFIN:100:6000 ;comment1",
                "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                "ALTOWNER:AAWHAT2:S ;comment3.1",
                "BORDER:AAFIN:AAWHAT ;comment4",
                "ARRAPT:EGAA:EGAC ;comment5",
                "DEPAPT:EGAA ;comment6",
                "ACTIVE:EGLL:09R ;comment6.5",
                "ACTIVE:EGLL:09L ;comment6.5.1",
                "GUEST:GDR:*:EGAA ;comment7",
            }}, // No OWNER defined

        };

        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            this.RunParserOnLines(lines);

            Assert.Empty(this.sectorElementCollection.Sectors);
            this.logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsData()
        {
            this.RunParserOnLines(
                new List<string>(new[] {
                    "SECTOR:AAFIN:100:6000 ;comment1",
                    "OWNER:AAF:AAR:STA ;comment2",
                    "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                    "ALTOWNER:AAWHAT2:S ;comment3.1",
                    "BORDER:AAFIN:AAWHAT ;comment4",
                    "ARRAPT:EGAA:EGAC ;comment5",
                    "ARRAPT:EGAE ;comment5.1",
                    "DEPAPT:EGKK:EGLL ;comment6",
                    "DEPAPT:EGLC ;comment6.1",
                    "ACTIVE:EGLL:09R ;comment6.5",
                    "ACTIVE:EGLL:09L ;comment6.5.1",
                    "GUEST:GDR:*:EGAA ;comment7",
                    "SECTOR:TCNW:0:7000 ;comment8",
                    "OWNER:TCNE:TCN:TC ;comment9",
                    "ALTOWNER:Observing London FIR:L ;comment10",
                    "BORDER:TCNE1 ;comment11",
                    "ARRAPT:EGSS ;comment12",
                    "DEPAPT:EGSS ;comment13",
                    "GUEST:SSR:*:* ;comment14",
                    "GUEST:SSR:*:EGSS ;comment14.1",
                    "SECTOR:Only AEAPP:0:0 ;comment15",
                    "OWNER:AEA ;comment16"
                })
            );

            // First - base data
            Sector result1 = this.sectorElementCollection.Sectors[0];
            Assert.Equal("AAFIN", result1.Name);
            Assert.Equal(100, result1.MinimumAltitude);
            Assert.Equal(6000, result1.MaximumAltitude);
            this.AssertExpectedMetadata(result1, 1, "comment1");
            
            // First - OWNER
            Assert.Equal(3, result1.Owners.Owners.Count);
            Assert.Equal("AAF", result1.Owners.Owners[0]);
            Assert.Equal("AAR", result1.Owners.Owners[1]);
            Assert.Equal("STA", result1.Owners.Owners[2]);
            this.AssertExpectedMetadata(result1.Owners, 2, "comment2");

            // First - ALTOWNER 1
            Assert.Equal(2, result1.AltOwners.Count);
            Assert.Equal("AAWHAT", result1.AltOwners[0].Name);
            Assert.Equal(3, result1.AltOwners[0].Owners.Count);
            Assert.Equal("SW", result1.AltOwners[0].Owners[0]);
            Assert.Equal("SWD", result1.AltOwners[0].Owners[1]);
            Assert.Equal("S", result1.AltOwners[0].Owners[2]);
            this.AssertExpectedMetadata(result1.AltOwners[0], 3, "comment3");
            
            // First - ALTOWNER 2
            Assert.Equal("AAWHAT2", result1.AltOwners[1].Name);
            Assert.Single(result1.AltOwners[1].Owners);
            Assert.Equal("S", result1.AltOwners[1].Owners[0]);
            this.AssertExpectedMetadata(result1.AltOwners[1], 4, "comment3.1");
            
            // First - BORDER
            Assert.Single(result1.Borders);
            Assert.Equal(2, result1.Borders[0].BorderLines.Count);
            Assert.Equal("AAFIN", result1.Borders[0].BorderLines[0]);
            Assert.Equal("AAWHAT", result1.Borders[0].BorderLines[1]);
            this.AssertExpectedMetadata(result1.Borders[0], 5, "comment4");

            // First - ARRAPT 1
            Assert.Equal(2, result1.ArrivalAirports.Count);
            Assert.Equal(2, result1.ArrivalAirports[0].Airports.Count);
            Assert.Equal("EGAA", result1.ArrivalAirports[0].Airports[0]);
            Assert.Equal("EGAC", result1.ArrivalAirports[0].Airports[1]);
            this.AssertExpectedMetadata(result1.ArrivalAirports[0], 6, "comment5");
            
            // First - ARRAPT 2
            Assert.Single(result1.ArrivalAirports[1].Airports);
            Assert.Equal("EGAE", result1.ArrivalAirports[1].Airports[0]);
            this.AssertExpectedMetadata(result1.ArrivalAirports[1], 7, "comment5.1");

            // First - DEPAPT 1
            Assert.Equal(2, result1.DepartureAirports.Count);
            Assert.Equal(2, result1.DepartureAirports[0].Airports.Count);
            Assert.Equal("EGKK", result1.DepartureAirports[0].Airports[0]);
            Assert.Equal("EGLL", result1.DepartureAirports[0].Airports[1]);
            this.AssertExpectedMetadata(result1.DepartureAirports[0], 8, "comment6");
            
            // First - DEPAPT 2
            Assert.Single(result1.DepartureAirports[1].Airports);
            Assert.Equal("EGLC", result1.DepartureAirports[1].Airports[0]);
            this.AssertExpectedMetadata(result1.DepartureAirports[1], 9, "comment6.1");
            
            // First - ACTIVE 1
            Assert.Equal(2, result1.Active.Count);
            Assert.Equal("EGLL", result1.Active[0].Airfield);
            Assert.Equal("09R", result1.Active[0].Runway);
            this.AssertExpectedMetadata(result1.Active[0], 10, "comment6.5");
            
            // First - ACTIVE 2
            Assert.Equal("EGLL", result1.Active[1].Airfield);
            Assert.Equal("09L", result1.Active[1].Runway);
            this.AssertExpectedMetadata(result1.Active[1], 11, "comment6.5.1");
            
            // First - GUEST
            Assert.Single(result1.Guests);
            Assert.Equal("GDR", result1.Guests[0].Controller);
            Assert.Equal("*", result1.Guests[0].DepartureAirport);
            Assert.Equal("EGAA", result1.Guests[0].ArrivalAirport);
            this.AssertExpectedMetadata(result1.Guests[0], 12, "comment7");

            // Second
            Sector result2 = this.sectorElementCollection.Sectors[1];
            Assert.Equal("TCNW", result2.Name);
            Assert.Equal(0, result2.MinimumAltitude);
            Assert.Equal(7000, result2.MaximumAltitude);
            this.AssertExpectedMetadata(result2, 13, "comment8");

            // Second - OWNER
            Assert.Equal(3, result2.Owners.Owners.Count);
            Assert.Equal("TCNE", result2.Owners.Owners[0]);
            Assert.Equal("TCN", result2.Owners.Owners[1]);
            Assert.Equal("TC", result2.Owners.Owners[2]);
            this.AssertExpectedMetadata(result2.Owners, 14, "comment9");
            
            // Second - ALTOWNER
            Assert.Single(result2.AltOwners);
            Assert.Equal("Observing London FIR", result2.AltOwners[0].Name);
            Assert.Single(result2.AltOwners[0].Owners);
            Assert.Equal("L", result2.AltOwners[0].Owners[0]);
            this.AssertExpectedMetadata(result2.AltOwners[0], 15, "comment10");
            
            // Second - BORDER
            Assert.Single(result2.Borders);
            Assert.Single(result2.Borders[0].BorderLines);
            Assert.Equal("TCNE1", result2.Borders[0].BorderLines[0]);
            this.AssertExpectedMetadata(result2.Borders[0], 16, "comment11");
            
            // Second - ARRAPT
            Assert.Single(result2.ArrivalAirports);
            Assert.Single(result2.ArrivalAirports[0].Airports);
            Assert.Equal("EGSS", result2.ArrivalAirports[0].Airports[0]);
            this.AssertExpectedMetadata(result2.ArrivalAirports[0], 17, "comment12");
            
            // Second - DEPAPT
            Assert.Single(result2.DepartureAirports);
            Assert.Single(result2.DepartureAirports[0].Airports);
            Assert.Equal("EGSS", result2.DepartureAirports[0].Airports[0]);
            this.AssertExpectedMetadata(result2.DepartureAirports[0], 18, "comment13");
            
            // Second - ACTIVE
            Assert.Empty(result2.Active);
            
            // Second - GUEST
            Assert.Equal(2, result2.Guests.Count);
            Assert.Equal("SSR", result2.Guests[0].Controller);
            Assert.Equal("*", result2.Guests[0].DepartureAirport);
            Assert.Equal("*", result2.Guests[0].ArrivalAirport);
            this.AssertExpectedMetadata(result2.Guests[0], 19, "comment14");
            
            Assert.Equal("SSR", result2.Guests[1].Controller);
            Assert.Equal("*", result2.Guests[1].DepartureAirport);
            Assert.Equal("EGSS", result2.Guests[1].ArrivalAirport);
            this.AssertExpectedMetadata(result2.Guests[1], 20, "comment14.1");

            // Third
            Sector result3 = this.sectorElementCollection.Sectors[2];
            Assert.Equal("Only AEAPP", result3.Name);
            Assert.Equal(0, result3.MinimumAltitude);
            Assert.Equal(0, result3.MaximumAltitude);
            this.AssertExpectedMetadata(result3, 21, "comment15");
            
            // Third - OWNER
            Assert.Single(result3.Owners.Owners);
            Assert.Equal("AEA", result3.Owners.Owners[0]);
            this.AssertExpectedMetadata(result3.Owners, 22, "comment16");
            
            // Third - The rest
            Assert.Empty(result3.AltOwners);
            Assert.Empty(result3.Borders);
            Assert.Empty(result3.DepartureAirports);
            Assert.Empty(result3.ArrivalAirports);
            Assert.Empty(result3.Active);
            Assert.Empty(result3.Guests);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.ESE_OWNERSHIP;
        }
    }
}
