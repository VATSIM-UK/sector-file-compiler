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
            sectorElements.Add(FixFactory.Make("testfix"));
            sectorElements.Add(VorFactory.Make("testvor"));
            sectorElements.Add(NdbFactory.Make("testndb"));
            sectorElements.Add(AirportFactory.Make("testairport"));
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

            sectorElements.Add(route);
            
            AssertNoValidationErrors();
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

            sectorElements.Add(route);
            sectorElements.Fixes.Clear();
            
            AssertValidationErrors();
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

            sectorElements.Add(route);
            sectorElements.Vors.Clear();
            
            AssertValidationErrors();
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

            sectorElements.Add(route);
            sectorElements.Ndbs.Clear();
            
            AssertValidationErrors(2);
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

            sectorElements.Add(route);
            sectorElements.Airports.Clear();

            AssertValidationErrors(2);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSctStarsMustHaveAValidRoute();
        }
    }
}
