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
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.rule = new AllSectorsMustHaveValidBorder();
            this.args = new CompilerArguments();

            this.sectorElements.Add(SectorlineFactory.Make("ONE"));
            this.sectorElements.Add(SectorlineFactory.Make("TWO"));
            this.sectorElements.Add(CircleSectorlineFactory.Make("THREE"));
        }

        [Fact]
        public void TestItPassesOnValidBorders()
        {
            this.sectorElements.Add(
                new Sector(
                    "COOL1",
                    5000,
                    66000,
                    SectorOwnerHierarchyFactory.Make(),
                    SectorAlternateOwnerHierarchyFactory.MakeList(2),
                    SectorActiveFactory.MakeList(),
                    SectorGuestFactory.MakeList(),
                    new List<SectorBorder>() {
                        new SectorBorder(
                            new List<string>()
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

            this.sectorElements.Add(
                new Sector(
                    "COOL2",
                    5000,
                    66000,
                    SectorOwnerHierarchyFactory.Make(),
                    SectorAlternateOwnerHierarchyFactory.MakeList(2),
                    SectorActiveFactory.MakeList(),
                    SectorGuestFactory.MakeList(),
                    new List<SectorBorder>() {
                        new SectorBorder(
                            new List<string>()
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

            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Theory]
        [InlineData("ONE","TWO", "WHAT")]
        [InlineData("THREE", "ONE", "NOPE")]
        [InlineData("THREE", "TWO", "FOUR")]
        public void TestItFailsOnInvalidBorder(string first, string second, string bad)
        {
            this.sectorElements.Add(
                new Sector(
                    "COOL1",
                    5000,
                    66000,
                    SectorOwnerHierarchyFactory.Make(),
                    SectorAlternateOwnerHierarchyFactory.MakeList(2),
                    SectorActiveFactory.MakeList(),
                    SectorGuestFactory.MakeList(),
                    new List<SectorBorder>() {
                        new SectorBorder(
                            new List<string>()
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

            this.sectorElements.Add(
                new Sector(
                    "COOL2",
                    5000,
                    66000,
                    SectorOwnerHierarchyFactory.Make(),
                    SectorAlternateOwnerHierarchyFactory.MakeList(2),
                    SectorActiveFactory.MakeList(),
                    SectorGuestFactory.MakeList(),
                    new List<SectorBorder>() {
                        new SectorBorder(
                            new List<string>()
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

            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
