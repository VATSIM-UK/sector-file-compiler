using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;

namespace CompilerTest.Validate
{
    public class AllSctStarsMustHaveAValidRouteTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllSctStarsMustHaveAValidRoute rule;

        public AllSctStarsMustHaveAValidRouteTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.sectorElements.Add(new Fix("testfix", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Vor("testvor", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Ndb("testndb", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Airport("testairport", "testairport", new Coordinate("abc", "def"), "123.456", "test"));
            this.rule = new AllSctStarsMustHaveAValidRoute();
        }

        [Fact]
        public void TestItPassesOnValidRoute()
        {
            List<RouteSegment> segments = new List<RouteSegment>
            {
                new RouteSegment(new Point("testfix"), new Point("testvor"), null),
                new RouteSegment(new Point("testvor"), new Point("testndb"), null),
                new RouteSegment(new Point("testndb"), new Point("testairport"), null),
                new RouteSegment(new Point("testairport"), new Point(new Coordinate("abc", "def")), null),
                new RouteSegment(new Point(new Coordinate("abc", "def")), new Point("testfix"), null),
            };

            SidStarRoute route = new SidStarRoute(
                SidStarType.STAR,
                "EGKK TEST",
                segments
            );

            this.sectorElements.Add(route);
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnBadFix()
        {
            List<RouteSegment> segments = new List<RouteSegment>
            {
                new RouteSegment(new Point("testfix"), new Point("testvor"), null),
                new RouteSegment(new Point("testvor"), new Point("testndb"), null),
                new RouteSegment(new Point("testndb"), new Point("testairport"), null),
                new RouteSegment(new Point("testairport"), new Point(new Coordinate("abc", "def")), null),
                new RouteSegment(new Point(new Coordinate("abc", "def")), new Point("testfix"), null),
            };

            SidStarRoute route = new SidStarRoute(
                SidStarType.STAR,
                "EGKK TEST",
                segments
            );

            this.sectorElements.Add(route);
            this.sectorElements.Fixes.Clear();

            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Exactly(2));
        }

        [Fact]
        public void TestItFailsOnBadVor()
        {
            List<RouteSegment> segments = new List<RouteSegment>
            {
                new RouteSegment(new Point("testfix"), new Point("testvor"), null),
                new RouteSegment(new Point("testvor"), new Point("testndb"), null),
                new RouteSegment(new Point("testndb"), new Point("testairport"), null),
                new RouteSegment(new Point("testairport"), new Point(new Coordinate("abc", "def")), null),
                new RouteSegment(new Point(new Coordinate("abc", "def")), new Point("testfix"), null),
            };

            SidStarRoute route = new SidStarRoute(
                SidStarType.STAR,
                "EGKK TEST",
                segments
            );

            this.sectorElements.Add(route);
            this.sectorElements.Vors.Clear();

            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Exactly(2));
        }

        [Fact]
        public void TestItFailsOnBadNdb()
        {
            List<RouteSegment> segments = new List<RouteSegment>
            {
                new RouteSegment(new Point("testfix"), new Point("testvor"), null),
                new RouteSegment(new Point("testvor"), new Point("testndb"), null),
                new RouteSegment(new Point("testndb"), new Point("testairport"), null),
                new RouteSegment(new Point("testairport"), new Point(new Coordinate("abc", "def")), null),
                new RouteSegment(new Point(new Coordinate("abc", "def")), new Point("testfix"), null),
            };

            SidStarRoute route = new SidStarRoute(
                SidStarType.STAR,
                "EGKK TEST",
                segments
            );

            this.sectorElements.Add(route);
            this.sectorElements.Ndbs.Clear();

            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Exactly(2));
        }

        [Fact]
        public void TestItFailsOnBadAirport()
        {
            List<RouteSegment> segments = new List<RouteSegment>
            {
                new RouteSegment(new Point("testfix"), new Point("testvor"), null),
                new RouteSegment(new Point("testvor"), new Point("testndb"), null),
                new RouteSegment(new Point("testndb"), new Point("testairport"), null),
                new RouteSegment(new Point("testairport"), new Point(new Coordinate("abc", "def")), null),
                new RouteSegment(new Point(new Coordinate("abc", "def")), new Point("testfix"), null),
            };

            SidStarRoute route = new SidStarRoute(
                SidStarType.STAR,
                "EGKK TEST",
                segments
            );

            this.sectorElements.Add(route);
            this.sectorElements.Airports.Clear();

            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Exactly(2));
        }
    }
}
