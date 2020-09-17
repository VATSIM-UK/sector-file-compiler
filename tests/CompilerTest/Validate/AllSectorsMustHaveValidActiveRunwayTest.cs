using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Model;
using Compiler.Validate;
using Compiler.Event;
using Compiler.Error;
using Compiler.Argument;

namespace CompilerTest.Validate
{
    public class AllSectorsMustHaveValidActiveRunwayTest
    {
        private readonly SectorElementCollection elements;
        private readonly AllSectorsMustHaveValidActiveRunway validator;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly CompilerArguments args;

        public AllSectorsMustHaveValidActiveRunwayTest()
        {
            this.elements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.validator = new AllSectorsMustHaveValidActiveRunway();
            this.args = new CompilerArguments();

            this.elements.Add(new Airport("a", "EGKK", new Coordinate("a", "b"), "a", "c"));
            this.elements.Add(new Airport("b", "EGLL", new Coordinate("a", "b"), "a", "c"));
            this.elements.Add(new Airport("c", "EGCC", new Coordinate("a", "b"), "a", "c"));
            this.elements.Add(
                new Runway("26L", 270, new Coordinate("abc", "def"), "09", 90, new Coordinate("abc", "def"), "EGKK a", "a")
            );
            this.elements.Add(
                new Runway("27L", 270, new Coordinate("abc", "def"), "09", 90, new Coordinate("abc", "def"), "EGLL b", "b")
            );
            this.elements.Add(
                new Runway("27R", 270, new Coordinate("abc", "def"), "09", 90, new Coordinate("abc", "def"), "EGLL b", "b")
            );
            this.elements.Add(
                new Runway("23L", 270, new Coordinate("abc", "def"), "09", 90, new Coordinate("abc", "def"), "EGCC c", "c")
            );
        }

        [Theory]
        [InlineData("EGCC", "23L", "EGLL", "27R")]
        [InlineData("EGLL", "27L", "EGLL", "27R")]
        [InlineData("EGKK", "26L", "EGCC", "23L")]
        [InlineData("EGCC", "23L", "EGCC", "23L")]
        [InlineData("000A", "00", "000A", "01")]
        public void TestItPassesOnAllValid(string firstAirport, string firstRunway, string secondAirport, string secondRunway)
        {
            this.elements.Add(
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
                        new SectorActive(firstAirport, firstRunway, ""),
                        new SectorActive(secondAirport, secondRunway, ""),
                    },
                    new List<SectorGuest>()
                    {
                        new SectorGuest("LLN", "EGLL", "EGWU", ""),
                        new SectorGuest("LLS", "EGLL", "*", ""),
                    },
                    new SectorBorder(
                    new List<string>()
                        {
                            "ONE",
                            "TWO",
                        },
                        ""
                    ),
                    new SectorArrivalAirports(new List<string>() { "EGSS", "EGGW" }, ""),
                    new SectorDepartureAirports(new List<string>() { "EGLL", "EGWU" }, ""),
                    "comment"
                )
            );

            this.elements.Add(
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
                        new SectorActive(firstAirport, firstRunway, ""),
                        new SectorActive(secondAirport, secondRunway, ""),
                    },
                    new List<SectorGuest>()
                    {
                        new SectorGuest("LLN", "EGLL", "EGWU", ""),
                        new SectorGuest("LLS", "EGLL", "*", ""),
                    },
                    new SectorBorder(
                    new List<string>()
                        {
                            "ONE",
                            "TWO",
                        },
                        ""
                    ),
                    new SectorArrivalAirports(new List<string>() { "EGSS", "EGGW" }, ""),
                    new SectorDepartureAirports(new List<string>() { "EGLL", "EGWU" }, ""),
                    "comment"
                )
            );

            this.validator.Validate(this.elements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Theory]
        [InlineData("EGCC", "23R", "EGLL", "27R", 2)]
        [InlineData("EGKK", "27L", "EGLL", "27R", 2)]
        [InlineData("EGKK", "26R", "EGKK", "26R", 2)] // Double failures only count once
        [InlineData("EGLL", "26R", "EGLL", "23L", 2)]
        [InlineData("000B", "01", "000A", "04", 2)]
        public void TestItFailsOnInvalid (string firstAirport, string firstRunway, string secondAirport, string secondRunway, int failTimes)
        {
            this.elements.Add(
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
                        new SectorActive(firstAirport, firstRunway, ""),
                        new SectorActive(secondAirport, secondRunway, ""),
                    },
                    new List<SectorGuest>()
                    {
                        new SectorGuest("LLN", "EGLL", "EGWU", ""),
                        new SectorGuest("LLS", "EGLL", "*", ""),
                    },
                    new SectorBorder(
                    new List<string>()
                        {
                            "ONE",
                            "TWO",
                        },
                        ""
                    ),
                    new SectorArrivalAirports(new List<string>() { "EGSS", "EGGW" }, ""),
                    new SectorDepartureAirports(new List<string>() { "EGLL", "EGWU" }, ""),
                    "comment"
                )
            );

            this.elements.Add(
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
                        new SectorActive(firstAirport, firstRunway, ""),
                        new SectorActive(secondAirport, secondRunway, ""),
                    },
                    new List<SectorGuest>()
                    {
                        new SectorGuest("LLN", "EGLL", "EGWU", ""),
                        new SectorGuest("LLS", "EGLL", "*", ""),
                    },
                    new SectorBorder(
                    new List<string>()
                        {
                            "ONE",
                            "TWO",
                        },
                        ""
                    ),
                    new SectorArrivalAirports(new List<string>() { "EGSS", "EGGW" }, ""),
                    new SectorDepartureAirports(new List<string>() { "EGLL", "EGWU" }, ""),
                    "comment"
                )
            );

            this.validator.Validate(this.elements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Exactly(failTimes));
        }
    }
}
