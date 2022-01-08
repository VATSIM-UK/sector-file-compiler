using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;
using Compiler.Argument;
using System.Collections.Generic;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSectorsBordersMustBeContiguousTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllSectorBordersMustBeContiguous rule;
        private readonly CompilerArguments args;

        public AllSectorsBordersMustBeContiguousTest()
        {
            sectorElements = new SectorElementCollection();
            loggerMock = new Mock<IEventLogger>();
            rule = new AllSectorBordersMustBeContiguous();
            args = new CompilerArguments();

            sectorElements.Add(
                SectorlineFactory.Make(
                    name: "ONE",
                    coordinates: new()
                    {
                        SectorlineCoordinateFactory.Make(new("a", "a")),
                        SectorlineCoordinateFactory.Make(new("b", "b")),
                        SectorlineCoordinateFactory.Make(new("c", "c")),
                    }
                )
            );
            sectorElements.Add(
                SectorlineFactory.Make(
                    name: "ONE_BACKWARDS",
                    coordinates: new()
                    {
                        SectorlineCoordinateFactory.Make(new("c", "c")),
                        SectorlineCoordinateFactory.Make(new("b", "b")),
                        SectorlineCoordinateFactory.Make(new("a", "a")),
                    }
                )
            );
            sectorElements.Add(
                SectorlineFactory.Make(
                    name: "TWO",
                    coordinates: new()
                    {
                        SectorlineCoordinateFactory.Make(new("c", "c")),
                        SectorlineCoordinateFactory.Make(new("d", "d")),
                        SectorlineCoordinateFactory.Make(new("e", "e")),
                    }
                )
            );
            sectorElements.Add(
                SectorlineFactory.Make(
                    name: "TWO_BACKWARDS",
                    coordinates: new()
                    {
                        SectorlineCoordinateFactory.Make(new("e", "e")),
                        SectorlineCoordinateFactory.Make(new("d", "d")),
                        SectorlineCoordinateFactory.Make(new("c", "c")),
                    }
                )
            );
            sectorElements.Add(
                SectorlineFactory.Make(
                    name: "THREE",
                    coordinates: new()
                    {
                        SectorlineCoordinateFactory.Make(new("g", "g")),
                        SectorlineCoordinateFactory.Make(new("f", "f")),
                        SectorlineCoordinateFactory.Make(new("e", "e")),
                    }
                )
            );
            sectorElements.Add(
                SectorlineFactory.Make(
                    name: "FOUR",
                    coordinates: new()
                    {
                        SectorlineCoordinateFactory.Make(new("g", "g")),
                        SectorlineCoordinateFactory.Make(new("h", "h")),
                        SectorlineCoordinateFactory.Make(new("a", "a")),
                    }
                )
            );
            sectorElements.Add(CircleSectorlineFactory.Make("FIVE"));
        }

        [Fact]
        public void TestItPassesOnValidBorderStartOfFirstLineMatchesEndOfLastLine()
        {
            sectorElements.Add(
                new Sector(
                    "COOL1",
                    5000,
                    66000,
                    SectorOwnerHierarchyFactory.Make(),
                    SectorAlternateOwnerHierarchyFactory.MakeList(2),
                    SectorActiveFactory.MakeList(),
                    SectorGuestFactory.MakeList(),
                    new List<SectorBorder>
                    {
                        new(
                            new List<string>
                            {
                                "ONE",
                                "TWO",
                                "THREE",
                                "FOUR",
                            },
                            DefinitionFactory.Make(),
                            DocblockFactory.Make(),
                            CommentFactory.Make()
                        )
                    },
                    SectorArrivalAirportsFactory.MakeList(),
                    SectorDepartureAirportsFactory.MakeList(),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            );

            rule.Validate(sectorElements, args, loggerMock.Object);
            loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItPassesOnValidBorderEndOfFirstLineMatchesEndOfLastLine()
        {
            sectorElements.Add(
                new Sector(
                    "COOL1",
                    5000,
                    66000,
                    SectorOwnerHierarchyFactory.Make(),
                    SectorAlternateOwnerHierarchyFactory.MakeList(2),
                    SectorActiveFactory.MakeList(),
                    SectorGuestFactory.MakeList(),
                    new List<SectorBorder>
                    {
                        new(
                            new List<string>
                            {
                                "THREE",
                                "FOUR",
                                "ONE",
                                "TWO"
                            },
                            DefinitionFactory.Make(),
                            DocblockFactory.Make(),
                            CommentFactory.Make()
                        )
                    },
                    SectorArrivalAirportsFactory.MakeList(),
                    SectorDepartureAirportsFactory.MakeList(),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            );
            rule.Validate(sectorElements, args, loggerMock.Object);
            loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItPassesOnValidBorderEndOfFirstLineMatchesStartOfLastLine()
        {
            sectorElements.Add(
                new Sector(
                    "COOL1",
                    5000,
                    66000,
                    SectorOwnerHierarchyFactory.Make(),
                    SectorAlternateOwnerHierarchyFactory.MakeList(2),
                    SectorActiveFactory.MakeList(),
                    SectorGuestFactory.MakeList(),
                    new List<SectorBorder>
                    {
                        new(
                            new List<string>
                            {
                                "THREE",
                                "FOUR",
                                "ONE",
                                "TWO_BACKWARDS"
                            },
                            DefinitionFactory.Make(),
                            DocblockFactory.Make(),
                            CommentFactory.Make()
                        )
                    },
                    SectorArrivalAirportsFactory.MakeList(),
                    SectorDepartureAirportsFactory.MakeList(),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            );
            rule.Validate(sectorElements, args, loggerMock.Object);
            loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItPassesOnValidBorderStartOfFirstLineMatchesStartOfLastLine()
        {
            sectorElements.Add(
                new Sector(
                    "COOL1",
                    5000,
                    66000,
                    SectorOwnerHierarchyFactory.Make(),
                    SectorAlternateOwnerHierarchyFactory.MakeList(2),
                    SectorActiveFactory.MakeList(),
                    SectorGuestFactory.MakeList(),
                    new List<SectorBorder>
                    {
                        new(
                            new List<string>
                            {
                                "TWO",
                                "THREE",
                                "FOUR",
                                "ONE_BACKWARDS",
                            },
                            DefinitionFactory.Make(),
                            DocblockFactory.Make(),
                            CommentFactory.Make()
                        )
                    },
                    SectorArrivalAirportsFactory.MakeList(),
                    SectorDepartureAirportsFactory.MakeList(),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            );
            rule.Validate(sectorElements, args, loggerMock.Object);
            loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItPassesOnSingleLineBorder()
        {
            sectorElements.Add(
                new Sector(
                    "COOL1",
                    5000,
                    66000,
                    SectorOwnerHierarchyFactory.Make(),
                    SectorAlternateOwnerHierarchyFactory.MakeList(2),
                    SectorActiveFactory.MakeList(),
                    SectorGuestFactory.MakeList(),
                    new List<SectorBorder>
                    {
                        new(
                            new List<string>
                            {
                                "TWO",
                            },
                            DefinitionFactory.Make(),
                            DocblockFactory.Make(),
                            CommentFactory.Make()
                        )
                    },
                    SectorArrivalAirportsFactory.MakeList(),
                    SectorDepartureAirportsFactory.MakeList(),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            );
            rule.Validate(sectorElements, args, loggerMock.Object);
            loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItPassesOnCircleBorder()
        {
            sectorElements.Add(
                new Sector(
                    "COOL1",
                    5000,
                    66000,
                    SectorOwnerHierarchyFactory.Make(),
                    SectorAlternateOwnerHierarchyFactory.MakeList(2),
                    SectorActiveFactory.MakeList(),
                    SectorGuestFactory.MakeList(),
                    new List<SectorBorder>
                    {
                        new(
                            new List<string>
                            {
                                "FIVE",
                            },
                            DefinitionFactory.Make(),
                            DocblockFactory.Make(),
                            CommentFactory.Make()
                        )
                    },
                    SectorArrivalAirportsFactory.MakeList(),
                    SectorDepartureAirportsFactory.MakeList(),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            );

            sectorElements.Add(
                new Sector(
                    "COOL2",
                    5000,
                    66000,
                    SectorOwnerHierarchyFactory.Make(),
                    SectorAlternateOwnerHierarchyFactory.MakeList(2),
                    SectorActiveFactory.MakeList(),
                    SectorGuestFactory.MakeList(),
                    new List<SectorBorder>
                    {
                        new(
                            new List<string>
                            {
                                "FIVE",
                            },
                            DefinitionFactory.Make(),
                            DocblockFactory.Make(),
                            CommentFactory.Make()
                        )
                    },
                    SectorArrivalAirportsFactory.MakeList(),
                    SectorDepartureAirportsFactory.MakeList(),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            );

            rule.Validate(sectorElements, args, loggerMock.Object);
            loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItPassesOnNoBorder()
        {
            sectorElements.Add(
                new Sector(
                    "COOL1",
                    5000,
                    66000,
                    SectorOwnerHierarchyFactory.Make(),
                    SectorAlternateOwnerHierarchyFactory.MakeList(2),
                    SectorActiveFactory.MakeList(),
                    SectorGuestFactory.MakeList(),
                    new List<SectorBorder>(),
                    SectorArrivalAirportsFactory.MakeList(),
                    SectorDepartureAirportsFactory.MakeList(),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            );

            sectorElements.Add(
                new Sector(
                    "COOL2",
                    5000,
                    66000,
                    SectorOwnerHierarchyFactory.Make(),
                    SectorAlternateOwnerHierarchyFactory.MakeList(2),
                    SectorActiveFactory.MakeList(),
                    SectorGuestFactory.MakeList(),
                    new List<SectorBorder>(),
                    SectorArrivalAirportsFactory.MakeList(),
                    SectorDepartureAirportsFactory.MakeList(),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            );

            rule.Validate(sectorElements, args, loggerMock.Object);
            loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Theory]
        [InlineData("ONE", "THREE", "FOUR")] // Break in middle between one and three
        [InlineData("ONE", "TWO", "FOUR")] // Break in middle between two and four
        [InlineData("ONE", "TWO", "THREE")] // Break between one and three
        [InlineData("ONE", "TWO_WUT", "THREE")] // Unknown border in the middle
        [InlineData("ONE_WUT", "TWO", "THREE")] // Unknown border at the start
        [InlineData("ONE", "TWO", "THREE_WUT")] // Unknown border at the start
        public void TestItFailsOnInvalidBorder(string first, string second, string third)
        {
            sectorElements.Add(
                new Sector(
                    "COOL1",
                    5000,
                    66000,
                    SectorOwnerHierarchyFactory.Make(),
                    SectorAlternateOwnerHierarchyFactory.MakeList(2),
                    SectorActiveFactory.MakeList(),
                    SectorGuestFactory.MakeList(),
                    new List<SectorBorder>
                    {
                        new(
                            new List<string>
                            {
                                first,
                                second,
                                third,
                            },
                            DefinitionFactory.Make(),
                            DocblockFactory.Make(),
                            CommentFactory.Make()
                        )
                    },
                    SectorArrivalAirportsFactory.MakeList(),
                    SectorDepartureAirportsFactory.MakeList(),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            );
            rule.Validate(sectorElements, args, loggerMock.Object);
            loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
