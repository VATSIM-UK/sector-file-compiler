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
    public class SectorParserTest
    {
        private readonly SectorParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public SectorParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (SectorParser)(new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.ESE_AIRSPACE, Subsections.ESE_AIRSPACE_SECTOR);
        }

        [Fact]
        public void TestItHandlesMetadata()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] {
                    "",
                    ";comment",
                    "SECTOR:Only AEAPP:0:0 ;comment15",
                    "",
                    "OWNER:AEA ;comment16"
                })
            );

            this.parser.ParseData(data);

            Assert.IsType<BlankLine>(
                this.collection.Compilables[OutputSections.ESE_AIRSPACE][Subsections.ESE_AIRSPACE_SECTOR][0]
            );
            Assert.IsType<CommentLine>(
                this.collection.Compilables[OutputSections.ESE_AIRSPACE][Subsections.ESE_AIRSPACE_SECTOR][1]
            );
            Assert.IsType<BlankLine>(
                this.collection.Compilables[OutputSections.ESE_AIRSPACE][Subsections.ESE_AIRSPACE_SECTOR][2]
            );
            Assert.IsType<Sector>(
                this.collection.Compilables[OutputSections.ESE_AIRSPACE][Subsections.ESE_AIRSPACE_SECTOR][3]
            );
        }

        [Fact]
        public void TestItAddsData()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] {
                    "SECTOR:AAFIN:100:6000 ;comment1",
                    "OWNER:AAF:AAR:STA ;comment2",
                    "ALTOWNER:AAWHAT:SW:SWD:S ;comment3",
                    "ALTOWNER:AAWHAT2:S ;comment3.1",
                    "BORDER:AAFIN:AAWHAT ;comment4",
                    "ARRAPT:EGAA:EGAC ;comment5",
                    "DEPAPT:EGAA ;comment6",
                    "GUEST:GDR:*:EGAA ;comment7",
                    "SECTOR:TCNW:0:7000 ;comment8",
                    "OWNER:TCNE:TCN:TC ;comment9",
                    "",
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

            this.parser.ParseData(data);

            // First
            Sector result1 = this.collection.Sectors[0];
            Assert.Equal("AAFIN", result1.Name);
            Assert.Equal(100, result1.MinimumAltitude);
            Assert.Equal(6000, result1.MaximumAltitude);
            Assert.Equal("comment1", result1.Comment);
            Assert.Equal(new SectorOwnerHierarchy(new List<string> {"AAF", "AAR", "STA" }, "comment2"), result1.Owners);
            Assert.Equal(
                new SectorAlternateOwnerHierarchy("AAWHAT", new List<string> { "SW", "SWD", "S" }, "comment3"),
                result1.AltOwners[0]
            );
            Assert.Equal(
                new SectorAlternateOwnerHierarchy("AAWHAT2", new List<string> { "S" }, "comment3.1"),
                result1.AltOwners[1]
            );
            Assert.Equal(2, result1.AltOwners.Count);
            Assert.Equal(new SectorBorder(new List<string> {"AAFIN", "AAWHAT"}, "comment4"), result1.Border);
            Assert.Equal(new SectorArrivalAirports(new List<string> { "EGAA", "EGAC" }, "comment5"), result1.ArrivalAirports);
            Assert.Equal(new SectorDepartureAirports(new List<string> { "EGAA" }, "comment6"), result1.DepartureAirports);
            Assert.Equal(new List<SectorGuest> { new SectorGuest("GDR", "*", "EGAA", "comment7") }, result1.Guests);


            // Second
            Sector result2 = this.collection.Sectors[1];
            Assert.Equal("TCNW", result2.Name);
            Assert.Equal(0, result2.MinimumAltitude);
            Assert.Equal(7000, result2.MaximumAltitude);
            Assert.Equal("comment8", result2.Comment);
            Assert.Equal(new SectorOwnerHierarchy(new List<string> { "TCNE", "TCN", "TC" }, "comment9"), result2.Owners);
            Assert.Equal(
                new SectorAlternateOwnerHierarchy("Observing London FIR", new List<string> { "L" }, "comment10"),
                result2.AltOwners[0]
            );
            Assert.Single(result2.AltOwners);
            Assert.Equal(new SectorBorder(new List<string> { "TCNE1" }, "comment11"), result2.Border);
            Assert.Equal(new SectorArrivalAirports(new List<string> { "EGSS" }, "comment12"), result2.ArrivalAirports);
            Assert.Equal(new SectorDepartureAirports(new List<string> { "EGSS" }, "comment13"), result2.DepartureAirports);
            Assert.Equal(
                new List<SectorGuest> { new SectorGuest("SSR", "*", "*", "comment14"), new SectorGuest("SSR", "*", "EGSS", "comment14.1") },
                result2.Guests
            );

            // Third
            Sector result3 = this.collection.Sectors[2];
            Assert.Equal("Only AEAPP", result3.Name);
            Assert.Equal(0, result3.MinimumAltitude);
            Assert.Equal(0, result3.MaximumAltitude);
            Assert.Equal("comment15", result3.Comment);
            Assert.Equal(new SectorOwnerHierarchy(new List<string> { "AEA" }, "comment16"), result3.Owners);
            Assert.Empty(result3.AltOwners);
            Assert.Empty(result3.Border.BorderLines);
            Assert.Empty(result3.ArrivalAirports.Airports);
            Assert.Empty(result3.DepartureAirports.Airports);
            Assert.Empty(result3.Guests);
        }
    }
}
