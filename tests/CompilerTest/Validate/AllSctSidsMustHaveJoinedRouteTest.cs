using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;

namespace CompilerTest.Validate
{
    public class AllSctStarsMustHaveJoinedRouteTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllSctStarsMustHaveJoinedRoute rule;
        private readonly SidStarRoute first;
        private readonly SidStarRoute second;

        public AllSctStarsMustHaveJoinedRouteTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            List<RouteSegment> segments1 = new List<RouteSegment>
            {
                new RouteSegment(new Point("testfix"), new Point("testvor"), null),
                new RouteSegment(new Point("testvor"), new Point("testndb"), null),
                new RouteSegment(new Point("testndb"), new Point("testairport"), null),
                new RouteSegment(new Point("testairport"), new Point(new Coordinate("abc", "def")), null),
                new RouteSegment(new Point(new Coordinate("abc", "def")), new Point("testfix"), null),
            };

            List<RouteSegment> segments2 = new List<RouteSegment>
            {
                new RouteSegment(new Point("testfix"), new Point("testvor"), null),
                new RouteSegment(new Point("testvor"), new Point("testndb"), null),
                new RouteSegment(new Point("testndb2"), new Point("testairport"), null),
                new RouteSegment(new Point("testairport"), new Point(new Coordinate("abc", "def")), null),
                new RouteSegment(new Point(new Coordinate("abc", "def")), new Point("testfix"), null),
            };
            this.first = new SidStarRoute(
                SidStarType.STAR,
                "EGKK TEST",
                segments1
            );

            this.second = new SidStarRoute(
                SidStarType.STAR,
                "EGKK TEST",
                segments2
            );

            this.rule = new AllSctStarsMustHaveJoinedRoute();
        }

        [Fact]
        public void TestItPassesOnValidRoute()
        {
            this.sectorElements.Add(first);
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnBadRoute()
        {
            this.sectorElements.Add(second);
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
