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
    public class AllSectorsMustHaveValidOwnerTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllSectorsMustHaveValidOwner rule;
        private readonly CompilerArguments args;

        public AllSectorsMustHaveValidOwnerTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.rule = new AllSectorsMustHaveValidOwner();
            this.args = new CompilerArguments();

            this.sectorElements.Add(
                new ControllerPosition(
                    "EGBB_APP",
                    "Birmingham Radar",
                    "123.970",
                    "BBR",
                    "B",
                    "EGBB",
                    "APP",
                    "0401",
                    "0407",
                    new List<Coordinate>(),
                    "comment"
                )
            );

            this.sectorElements.Add(
                new ControllerPosition(
                    "EGBB_F_APP",
                    "Birmingham Radar",
                    "123.970",
                    "BBF",
                    "B",
                    "EGBB",
                    "APP",
                    "0401",
                    "0407",
                    new List<Coordinate>(),
                    "comment"
                )
            );

            this.sectorElements.Add(
                new ControllerPosition(
                    "LON_S_CTR",
                    "London Control",
                    "123.970",
                    "LS",
                    "B",
                    "EGBB",
                    "APP",
                    "0401",
                    "0407",
                    new List<Coordinate>(),
                    "comment"
                )
            );

            this.sectorElements.Add(
                new ControllerPosition(
                    "LON_C_CTR",
                    "London Control",
                    "123.970",
                    "LC",
                    "B",
                    "EGBB",
                    "APP",
                    "0401",
                    "0407",
                    new List<Coordinate>(),
                    "comment"
                )
            );
        }

        [Fact]
        public void TestItPassesOnValidControllers()
        {
            this.sectorElements.Add(
                new Sector(
                    "COOL1",
                    5000,
                    66000,
                    new SectorOwnerHierarchy(
                        new List<string>()
                        {
                            "LS", "LC"
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
                            "LS", "LC", "BBR", "BBF"
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

            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Theory]
        [InlineData("LS","WHAT")]
        [InlineData("LC", "WHAT")]
        [InlineData("BBR", "WHAT")]
        [InlineData("BBF","WHAT")]
        public void TestItFailsOnInvalidControllers(string first, string second)
        {
            this.sectorElements.Add(
                new Sector(
                    "COOL1",
                    5000,
                    66000,
                    new SectorOwnerHierarchy(
                        new List<string>()
                        {
                            first
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
                            first, second
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

            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
