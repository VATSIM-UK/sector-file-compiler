using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;
using Compiler.Argument;

namespace CompilerTest.Validate
{
    public class AllCoordinationPointsMustHaveValidArrivalRunwaysTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllCoordinationPointsMustHaveValidArrivalRunways rule;
        private readonly CompilerArguments args;

        public AllCoordinationPointsMustHaveValidArrivalRunwaysTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();

            this.sectorElements.Add(
                new Airport("testairport1", "EGKK", new Coordinate("a", "b"), "a", "c")
            );
            this.sectorElements.Add(
                new Runway("26L", 270, new Coordinate("abc", "def"), "09", 90, new Coordinate("abc", "def"), "EGKK testairport1", "")
            );
            this.sectorElements.Add(
                new Airport("testairport2", "EGLL", new Coordinate("a", "b"), "a", "c")
            );
            this.sectorElements.Add(
                new Runway("09R", 270, new Coordinate("abc", "def"), "09", 90, new Coordinate("abc", "def"), "EGLL testairport2", "")
            );
            this.sectorElements.Add(
                new Runway("09L", 270, new Coordinate("abc", "def"), "09", 90, new Coordinate("abc", "def"), "EGLL testairport2", "")
            );
            this.sectorElements.Add(
                new Airport("yetanotherairport", "EGSS", new Coordinate("a", "b"), "a", "c")
            );
            this.sectorElements.Add(
                new Runway("04", 270, new Coordinate("abc", "def"), "09", 90, new Coordinate("abc", "def"), "EGSS notyetanothertestairport", "")
            );

            this.rule = new AllCoordinationPointsMustHaveValidArrivalRunways();
            this.args = new CompilerArguments();
        }

        [Theory]
        [InlineData("EGKK", "26L")]
        [InlineData("EGLL", "09R")]
        [InlineData("EGLL", "09L")]
        [InlineData("EGLL", "*")]
        [InlineData("EGSS", "*")]
        [InlineData("*", "09R")] // Should never happen
        [InlineData("*", "*")]

        public void TestItPassesOnValidDepartureRunway(string airport, string runway)
        {
            this.sectorElements.Add(
                new CoordinationPoint(
                    true,
                    "EGKK",
                    "26L",
                    "ABTUM",
                    airport,
                    runway,
                    "TCE",
                    "TCSW",
                    "*",
                    "14000",
                    "ABTUMDES",
                    "comment"
                )
            );
            this.sectorElements.Add(
                new CoordinationPoint(
                    true,
                    "EGKK",
                    "26L",
                    "ABTUM",
                    airport,
                    runway,
                    "TCE",
                    "TCSW",
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
        [InlineData("EGGW", "*")] // Airport doesnt exist
        [InlineData("EGLL", "27R")]
        [InlineData("EGLL", "27L")]
        [InlineData("EGKK", "08R")]
        [InlineData("EGKK", "08L")]
        [InlineData("EGSS", "04")] // Doesn't match up on the descriptions
        public void TestItFailsOnInvalidDepartureRunway(string airport, string runway)
        {
            this.sectorElements.Add(
                new CoordinationPoint(
                    true,
                    "EGKK",
                    "26L",
                    "ABTUM",
                    airport,
                    runway,
                    "TCE",
                    "TCSW",
                    "*",
                    "14000",
                    "ABTUMDES",
                    "comment"
                )
            );
            this.sectorElements.Add(
                new CoordinationPoint(
                    true,
                    "EGKK",
                    "26L",
                    "ABTUM",
                    airport,
                    runway,
                    "TCE",
                    "TCSW",
                    "*",
                    "14000",
                    "ARNUN",
                    "comment"
                )
            );
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Exactly(2));
        }
    }
}
