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
    public class AllSectorsMustHaveValidBorderTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllSectorsMustHaveValidBorder rule;
        private readonly CompilerArguments args;

        public AllSectorsMustHaveValidBorderTest()
        {
            sectorElements = new SectorElementCollection();
            loggerMock = new Mock<IEventLogger>();
            rule = new AllSectorsMustHaveValidBorder();
            args = new CompilerArguments();

            sectorElements.Add(SectorlineFactory.Make("ONE"));
            sectorElements.Add(SectorlineFactory.Make("TWO"));
            sectorElements.Add(CircleSectorlineFactory.Make("THREE"));
        }

        [Fact]
        public void TestItPassesOnValidBorders()
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
                                "ONE",
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

            rule.Validate(sectorElements, args, loggerMock.Object);
            loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Theory]
        [InlineData("ONE","TWO", "WHAT")]
        [InlineData("THREE", "ONE", "NOPE")]
        [InlineData("THREE", "TWO", "FOUR")]
        public void TestItFailsOnInvalidBorder(string first, string second, string bad)
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
                                first,
                                second,
                                bad
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
