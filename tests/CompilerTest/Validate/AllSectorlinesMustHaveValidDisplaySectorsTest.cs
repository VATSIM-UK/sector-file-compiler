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
    public class AllSectorlinesMustHaveValidDisplaySectorsTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllSectorlinesMustHaveValidDisplaySectors rule;
        private readonly CompilerArguments args;

        public AllSectorlinesMustHaveValidDisplaySectorsTest()
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

            this.rule = new AllSectorlinesMustHaveValidDisplaySectors();
            this.args = new CompilerArguments();
        }

        [Theory]
        [InlineData("COOL1", "COOL1", "COOL1", "COOL2", "COOL1", "COOL2")]
        [InlineData("COOL1", "COOL2", "COOL1", "COOL2", "COOL1", "COOL2")]
        [InlineData("COOL1", "COOL1", "COOL2", "COOL2", "COOL2", "COOL1")]
        public void TestItPassesOnValidSector(string oneA, string twoA, string threeA, string oneB, string twoB, string threeB)
        {
            this.sectorElements.Add(
                new Sectorline(
                    "ONE",
                    new List<SectorlineDisplayRule>
                    {
                        new SectorlineDisplayRule(oneA, twoA, threeA, "comment1"),
                        new SectorlineDisplayRule(oneB, twoB, threeB, "comment2")
                    },
                    new List<SectorlineCoordinate>
                    {
                        new SectorlineCoordinate(new Coordinate("abc", "def"), "comment3"),
                        new SectorlineCoordinate(new Coordinate("ghi", "jkl"), "comment4"),
                    },
                    "commentname"
                )
            );
            this.sectorElements.Add(
                new Sectorline(
                    "TWO",
                    new List<SectorlineDisplayRule>
                    {
                                    new SectorlineDisplayRule(oneA, twoA, threeA, "comment1"),
                                    new SectorlineDisplayRule(oneB, twoB, threeB, "comment2")
                    },
                    new List<SectorlineCoordinate>
                    {
                                    new SectorlineCoordinate(new Coordinate("abc", "def"), "comment3"),
                                    new SectorlineCoordinate(new Coordinate("ghi", "jkl"), "comment4"),
                    },
                    "commentname"
                )
            );

            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Theory]
        [InlineData("NOTCOOL1", "COOL1", "COOL1", "COOL2", "COOL1", "COOL2")]
        [InlineData("COOL1", "NOTCOOL2", "COOL1", "COOL2", "COOL1", "COOL2")]
        [InlineData("COOL1", "COOL1", "NOTCOOL2", "COOL2", "COOL2", "COOL1")]
        [InlineData("COOL1", "COOL1", "COOL2", "NOTCOOL2", "COOL2", "COOL1")]
        [InlineData("COOL1", "COOL1", "COOL2", "COOL2", "NOTCOOL2", "COOL1")]
        [InlineData("COOL1", "COOL1", "COOL2", "COOL2", "COOL2", "NOTCOOL1")]
        public void TestItFailsOnInvalidSector(string oneA, string twoA, string threeA, string oneB, string twoB, string threeB)
        {
            this.sectorElements.Add(
                new Sectorline(
                    "ONE",
                    new List<SectorlineDisplayRule>
                    {
                        new SectorlineDisplayRule(oneA, twoA, threeA, "comment1"),
                    },
                    new List<SectorlineCoordinate>
                    {
                        new SectorlineCoordinate(new Coordinate("abc", "def"), "comment3"),
                        new SectorlineCoordinate(new Coordinate("ghi", "jkl"), "comment4"),
                    },
                    "commentname"
                )
            );
            this.sectorElements.Add(
                new Sectorline(
                    "TWO",
                    new List<SectorlineDisplayRule>
                    {
                        new SectorlineDisplayRule(oneB, twoB, threeB, "comment2")
                    },
                    new List<SectorlineCoordinate>
                    {
                        new SectorlineCoordinate(new Coordinate("abc", "def"), "comment3"),
                        new SectorlineCoordinate(new Coordinate("ghi", "jkl"), "comment4"),
                    },
                    "commentname"
                )
            );

            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
