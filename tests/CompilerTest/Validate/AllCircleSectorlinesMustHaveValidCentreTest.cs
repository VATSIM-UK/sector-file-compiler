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
    public class AllCircleSectorlinesMustHaveValidCentreTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllCircleSectorlinesMustHaveValidCentre rule;
        private readonly CompilerArguments args;

        public AllCircleSectorlinesMustHaveValidCentreTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();

            this.sectorElements.Add(new Fix("testfix", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Vor("testvor", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Ndb("testndb", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Airport("testairport", "testairport", new Coordinate("abc", "def"), "123.456", "test"));

            this.rule = new AllCircleSectorlinesMustHaveValidCentre();
            this.args = new CompilerArguments();
        }

        [Theory]
        [InlineData("testfix")]
        [InlineData("testvor")]
        [InlineData("testndb")]
        [InlineData("testairport")]
        public void TestItPassesOnValidFix(string fix)
        {
            this.sectorElements.Add(
                new CircleSectorline(
                    "ONE",
                    fix,
                    5.5,
                    new List<SectorlineDisplayRule>(),
                    "commentname"
                )
            );
            this.sectorElements.Add(
                new CircleSectorline(
                    "TWO",
                    fix,
                    5.5,
                    new List<SectorlineDisplayRule>(),
                    "commentname"
                )
            );

            // This one is ignored by the rule
            this.sectorElements.Add(
                new CircleSectorline(
                    "ONE",
                    new Coordinate("abc", "def"),
                    5.5,
                    new List<SectorlineDisplayRule>(),
                    "commentname"
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
        public void TestItFailsOnInvalidFix(string firstFix, string secondFix)
        {
            this.sectorElements.Add(
                new CircleSectorline(
                    "ONE",
                    firstFix,
                    5.5,
                    new List<SectorlineDisplayRule>(),
                    "commentname"
                )
            );
            this.sectorElements.Add(
                new CircleSectorline(
                    "TWO",
                    secondFix,
                    5.5,
                    new List<SectorlineDisplayRule>(),
                    "commentname"
                )
            );

            // This one is ignored by the rule
            this.sectorElements.Add(
                new CircleSectorline(
                    "ONE",
                    new Coordinate("abc", "def"),
                    5.5,
                    new List<SectorlineDisplayRule>(),
                    "commentname"
                )
            );

            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
