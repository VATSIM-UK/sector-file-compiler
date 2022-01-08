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
    public class AllSectorsBordersMustBeSingleIfCircleTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllSectorsBordersMustBeSingleIfCircle rule;
        private readonly CompilerArguments args;

        public AllSectorsBordersMustBeSingleIfCircleTest()
        {
            sectorElements = new SectorElementCollection();
            loggerMock = new Mock<IEventLogger>();
            rule = new AllSectorsBordersMustBeSingleIfCircle();
            args = new CompilerArguments();

            sectorElements.Add(SectorlineFactory.Make("ONE"));
            sectorElements.Add(SectorlineFactory.Make("TWO"));
            sectorElements.Add(CircleSectorlineFactory.Make("THREE"));
            sectorElements.Add(CircleSectorlineFactory.Make("FOUR"));
        }

        [Fact]
        public void TestItPassesOnValidSectorlineBorder()
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
                                "TWO",
                                "ONE",
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
        public void TestItPassesOnValidCircleBorder()
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

        [Fact]
        public void TestItPassesOnMultipleBorders()
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
                            },
                            DefinitionFactory.Make(),
                            DocblockFactory.Make(),
                            CommentFactory.Make()
                        ),
                        new(
                            new List<string>
                            {
                                "TWO",
                                "ONE",
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
                                "TWO",
                                "ONE",
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

        [Theory]
        [InlineData("THREE", "FOUR")] // Two circles
        [InlineData("FOUR", "THREE")] // Two circles
        [InlineData("ONE", "THREE")] // Cicle and line
        [InlineData("THREE", "ONE")] // Cicle and line
        public void TestItFailsOnInvalidBorder(string first, string second)
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
