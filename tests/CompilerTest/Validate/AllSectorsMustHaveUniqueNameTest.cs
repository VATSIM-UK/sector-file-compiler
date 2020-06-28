using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;
using Compiler.Argument;

namespace CompilerTest.Validate
{
    public class AllSectorsMustHaveUniqueNameTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly Sector first;
        private readonly Sector second;
        private readonly Sector third;
        private readonly AllSectorsMustHaveUniqueName rule;
        private readonly CompilerArguments args;

        public AllSectorsMustHaveUniqueNameTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.first = new Sector(
                "ONE",
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
            );

            this.second = new Sector(
                "ONE",
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
            );

            this.third = new Sector(
                "NOTONE",
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
            );
            this.rule = new AllSectorsMustHaveUniqueName();
            this.args = new CompilerArguments();
        }

        [Fact]
        public void TestItPassesIfAllNamesDifferent()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.third);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnNameClash()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.second);
            this.sectorElements.Add(this.third);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
