using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;
using Compiler.Argument;

namespace CompilerTest.Validate
{
    public class AllCoordinationPointsMustHaveValidNextTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllCoordinationPointsMustHaveValidNext rule;
        private readonly CompilerArguments args;

        public AllCoordinationPointsMustHaveValidNextTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();

            this.sectorElements.Add(new Fix("testfix", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Vor("testvor", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Ndb("testndb", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Airport("testairport", "testairport", new Coordinate("abc", "def"), "123.456", "test"));

            this.rule = new AllCoordinationPointsMustHaveValidNext();
            this.args = new CompilerArguments();
        }

        [Theory]
        [InlineData("testfix")]
        [InlineData("*")]
        [InlineData("testvor")]
        [InlineData("testndb")]
        [InlineData("testairport")]
        public void TestItPassesOnValidNext(string fix)
        {
            this.sectorElements.Add(
                new CoordinationPoint(
                    true,
                    "WHAT",
                    "*",
                    "ABTUM",
                    fix,
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
                    "WHAT",
                    "*",
                    "ARNUN",
                    fix,
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
        [InlineData("nottestndb", "*")]
        [InlineData("testairport", "nottestairport")]
        public void TestItFailsOnInvalidNext(string firstFix, string secondFix)
        {
            this.sectorElements.Add(
                new CoordinationPoint(
                    true,
                    "WHAT",
                    "*",
                    "ABTUM",
                    firstFix,
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
                    "WHAT",
                    "*",
                    "ARNUN",
                    secondFix,
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
