using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;

namespace CompilerTest.Validate
{
    public class AllSidsMustHaveAValidRouteTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly SidStar first;
        private readonly SidStar second;
        private readonly AllSidsMustHaveAValidRoute rule;

        public AllSidsMustHaveAValidRouteTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.first = new SidStar(
                "SID",
                "EGKK",
                "26L",
                "ADMAG2X",
                new List<string>(new string[] { "testfix", "testvor", "testndb", "testairport" }),
                "test"
            );
            this.second = new SidStar(
                "SID",
                "EGKK",
                "26L",
                "ADMAG2X",
                new List<string>(new string[] { "nottestfix", "testvor", "testndb", "testairport" }),
                "test"
            );

            this.sectorElements.Add(new Fix("testfix", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Vor("testvor", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Ndb("testndb", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Airport("testairport", "testairport", new Coordinate("abc", "def"), "123.456", "test"));

            this.rule = new AllSidsMustHaveAValidRoute();
        }

        [Fact]
        public void TestItPassesOnValidRoute()
        {
            this.sectorElements.Add(this.first);
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidRoute()
        {
            this.sectorElements.Add(this.second);
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
