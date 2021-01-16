using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSctStarsMustHaveAValidRouteTest: AbstractValidatorTestCase
    {
        public AllSctStarsMustHaveAValidRouteTest()
        {
            this.sectorElements.Add(FixFactory.Make("testfix"));
            this.sectorElements.Add(VorFactory.Make("testvor"));
            this.sectorElements.Add(NdbFactory.Make("testndb"));
            this.sectorElements.Add(AirportFactory.Make("testairport"));
        }

        [Fact]
        public void TestItPassesOnValidRoute()
        {
            List<RouteSegment> segments = new()
            {
                RouteSegmentFactory.MakeDoublePoint("testvor", "testndb"),
                RouteSegmentFactory.MakeDoublePoint("testndb", "testairport"),
                RouteSegmentFactory.MakePointCoordinate("testairport"),
                RouteSegmentFactory.MakeCoordinatePoint("testfix"),
            };

            SidStarRoute route = new(
                SidStarType.STAR,
                "EGKK TEST",
                RouteSegmentFactory.MakeDoublePoint("testfix", "testvor"),
                segments,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );

            this.sectorElements.Add(route);
            
            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnBadFix()
        {
            List<RouteSegment> segments = new()
            {
                RouteSegmentFactory.MakeDoublePoint("testvor", "testndb"),
                RouteSegmentFactory.MakeDoublePoint("testndb", "testairport"),
                RouteSegmentFactory.MakePointCoordinate("testairport"),
                RouteSegmentFactory.MakeCoordinatePoint("testfix"),
            };

            SidStarRoute route = new(
                SidStarType.STAR,
                "EGKK TEST",
                RouteSegmentFactory.MakeDoublePoint("testfix", "testvor"),
                segments,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );

            this.sectorElements.Add(route);
            this.sectorElements.Fixes.Clear();
            
            this.AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnBadVor()
        {
            List<RouteSegment> segments = new()
            {
                RouteSegmentFactory.MakeDoublePoint("testvor", "testndb"),
                RouteSegmentFactory.MakeDoublePoint("testndb", "testairport"),
                RouteSegmentFactory.MakePointCoordinate("testairport"),
                RouteSegmentFactory.MakeCoordinatePoint("testfix"),
            };

            SidStarRoute route = new(
                SidStarType.STAR,
                "EGKK TEST",
                RouteSegmentFactory.MakeDoublePoint("testfix", "testvor"),
                segments,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );

            this.sectorElements.Add(route);
            this.sectorElements.Vors.Clear();
            
            this.AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnBadNdb()
        {
            List<RouteSegment> segments = new()
            {
                RouteSegmentFactory.MakeDoublePoint("testvor", "testndb"),
                RouteSegmentFactory.MakeDoublePoint("testndb", "testairport"),
                RouteSegmentFactory.MakePointCoordinate("testairport"),
                RouteSegmentFactory.MakeCoordinatePoint("testfix"),
            };

            SidStarRoute route = new(
                SidStarType.STAR,
                "EGKK TEST",
                RouteSegmentFactory.MakeDoublePoint("testfix", "testvor"),
                segments,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );

            this.sectorElements.Add(route);
            this.sectorElements.Ndbs.Clear();
            
            this.AssertValidationErrors(2);
        }

        [Fact]
        public void TestItFailsOnBadAirport()
        {
            List<RouteSegment> segments = new()
            {
                RouteSegmentFactory.MakeDoublePoint("testvor", "testndb"),
                RouteSegmentFactory.MakeDoublePoint("testndb", "testairport"),
                RouteSegmentFactory.MakePointCoordinate("testairport"),
                RouteSegmentFactory.MakeCoordinatePoint("testfix"),
            };

            SidStarRoute route = new(
                SidStarType.STAR,
                "EGKK TEST",
                RouteSegmentFactory.MakeDoublePoint("testfix", "testvor"),
                segments,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );

            this.sectorElements.Add(route);
            this.sectorElements.Airports.Clear();

            this.AssertValidationErrors(2);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSctStarsMustHaveAValidRoute();
        }
    }
}
