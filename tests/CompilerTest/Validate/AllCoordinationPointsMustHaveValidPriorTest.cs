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
    public class AllCoordinationPointsMustHaveValidPriorTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllCoordinationPointsMustHaveValidPrior rule;
        private readonly CompilerArguments args;

        public AllCoordinationPointsMustHaveValidPriorTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();

            this.sectorElements.Add(new Fix("testfix", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Vor("testvor", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Ndb("testndb", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Airport("testairport", "testairport", new Coordinate("abc", "def"), "123.456", "test"));

            this.rule = new AllCoordinationPointsMustHaveValidPrior();
            this.args = new CompilerArguments();
        }

        [Theory]
        [InlineData("testfix")]
        [InlineData("testvor")]
        [InlineData("testndb")]
        [InlineData("testairport")]
        public void TestItPassesOnValidRoute(string fix)
        {
            this.sectorElements.Add(
                new CoordinationPoint(
                    true,
                    fix,
                    "*",
                    "ABTUM",
                    "EGKK",
                    "26L",
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
                    fix,
                    "*",
                    "ARNUN",
                    "EGKK",
                    "26L",
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
        [InlineData("nottestfix","testfix")]
        [InlineData("testvor", "nottestvor")]
        [InlineData("nottestndb", "testndb")]
        [InlineData("testairport", "nottestairport")]
        public void TestItFailsOnInvalidRoute(string firstFix, string secondFix)
        {
            this.sectorElements.Add(
                new CoordinationPoint(
                    true,
                    firstFix,
                    "*",
                    "ABTUM",
                    "EGKK",
                    "26L",
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
                    secondFix,
                    "*",
                    "ARNUN",
                    "EGKK",
                    "26L",
                    "TCE",
                    "TCSW",
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
