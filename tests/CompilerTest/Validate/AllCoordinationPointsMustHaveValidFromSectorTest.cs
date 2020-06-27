using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;
using Compiler.Argument;
using System.Collections.Generic;

namespace CompilerTest.Validate
{
    public class AllCoordinationPointsMustHaveValidFromSectorTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllCoordinationPointsMustHaveValidFromSector rule;
        private readonly CompilerArguments args;

        public AllCoordinationPointsMustHaveValidFromSectorTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();


            this.sectorElements.Add(
                new Sector(
                    "COOL1",
                    5000,
                    66000,
                    new SectorOwnerHierarchy(
                        new List<string>()
                        {
                            "LLN", "LLS"
                        },
                        ""
                    ),
                    new List<SectorAlternateOwnerHierarchy>()
                    {
                        new SectorAlternateOwnerHierarchy("ALT1", new List<string>(){"LON1", "LON2"}, ""),
                        new SectorAlternateOwnerHierarchy("ALT2", new List<string>(){"LON3", "LON4"}, ""),
                    },
                    new List<SectorActive>()
                    {
                        new SectorActive("EGLL", "09R", ""),
                        new SectorActive("EGWU", "05", ""),
                    },
                    new List<SectorGuest>()
                    {
                        new SectorGuest("LLN", "EGLL", "EGWU", ""),
                        new SectorGuest("LLS", "EGLL", "*", ""),
                    },
                    new SectorBorder(
                    new List<string>()
                        {
                            "LINE1",
                            "LINE2",
                        },
                        ""
                    ),
                    new SectorArrivalAirports(new List<string>() { "EGSS", "EGGW" }, ""),
                    new SectorDepartureAirports(new List<string>() { "EGLL", "EGWU" }, ""),
                    "comment"
                )
            );

            this.sectorElements.Add(
                new Sector(
                    "COOL2",
                    5000,
                    66000,
                    new SectorOwnerHierarchy(
                        new List<string>()
                        {
                            "LLN", "LLS"
                        },
                        ""
                    ),
                    new List<SectorAlternateOwnerHierarchy>()
                    {
                        new SectorAlternateOwnerHierarchy("ALT1", new List<string>(){"LON1", "LON2"}, ""),
                        new SectorAlternateOwnerHierarchy("ALT2", new List<string>(){"LON3", "LON4"}, ""),
                    },
                    new List<SectorActive>()
                    {
                        new SectorActive("EGLL", "09R", ""),
                        new SectorActive("EGWU", "05", ""),
                    },
                    new List<SectorGuest>()
                    {
                        new SectorGuest("LLN", "EGLL", "EGWU", ""),
                        new SectorGuest("LLS", "EGLL", "*", ""),
                    },
                    new SectorBorder(
                    new List<string>()
                        {
                            "LINE1",
                            "LINE2",
                        },
                        ""
                    ),
                    new SectorArrivalAirports(new List<string>() { "EGSS", "EGGW" }, ""),
                    new SectorDepartureAirports(new List<string>() { "EGLL", "EGWU" }, ""),
                    "comment"
                )
            );

            this.rule = new AllCoordinationPointsMustHaveValidFromSector();
            this.args = new CompilerArguments();
        }

        [Theory]
        [InlineData("COOL1")]
        [InlineData("COOL2")]
        public void TestItPassesOnValidToSector(string sector)
        {
            this.sectorElements.Add(
                new CoordinationPoint(
                    true,
                    "WHAT",
                    "*",
                    "TEST",
                    "ABTUM",
                    "26L",
                    sector,
                    "TCE",
                    "*",
                    "14000",
                    "ABTUMDES",
                    "comment"
                )
            );
            this.sectorElements.Add(
                new CoordinationPoint(
                    true,
                    "WHAT",
                    "*",
                    "TEST",
                    "ARNUN",
                    "26L",
                    sector,
                    "TCE",
                    "*",
                    "14000",
                    "ARNUN",
                    "comment"
                )
            );
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Theory]
        [InlineData("COOL1","NOTCOOL1")]
        [InlineData("NOTCOOL2","COOL2")]
        public void TestItFailsOnInvalidToSector(string firstSector, string secondSector)
        {
            this.sectorElements.Add(
                new CoordinationPoint(
                    true,
                    "WHAT",
                    "*",
                    "WHAT",
                    "ABTUM",
                    "26L",
                    firstSector,
                    "TCE",
                    "*",
                    "14000",
                    "ABTUMDES",
                    "comment"
                )
            );
            this.sectorElements.Add(
                new CoordinationPoint(
                    true,
                    "WHAT",
                    "*",
                    "WHAT",
                    "ARNUN",
                    "26L",
                    secondSector,
                    "TCE",
                    "*",
                    "14000",
                    "ARNUN",
                    "comment"
                )
            );

            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
