using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSctSidsMustHaveAValidRouteTest: AbstractValidatorTestCase
    {
        public AllSctSidsMustHaveAValidRouteTest()
        {
            sectorElements.Add(FixFactory.Make("testfix"));
            sectorElements.Add(VorFactory.Make("testvor"));
            sectorElements.Add(NdbFactory.Make("testndb"));
            sectorElements.Add(AirportFactory.Make("testairport"));
        }

        [Fact]
        public void TestItPassesOnValidRoute()
        {
            sectorElements.Add(
                SidStarRouteFactory.Make(
                    segments: new List<RouteSegment>
                    {
                        RouteSegmentFactory.MakeDoublePoint("testfix", "testvor"),
                        RouteSegmentFactory.MakeDoublePoint("testvor", "testndb"),
                        RouteSegmentFactory.MakeDoublePoint("testndb", "testairport"),
                        RouteSegmentFactory.MakePointCoordinate("testairport", new Coordinate("abc", "def")),
                        RouteSegmentFactory.MakeCoordinatePoint("testvor", new Coordinate("abc", "def")),
                    }
                )
            );
           
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnBadFix()
        {
            sectorElements.Add(
                SidStarRouteFactory.Make(
                    segments: new List<RouteSegment>
                    {
                        RouteSegmentFactory.MakeDoublePoint("testfix", "testvor"),
                        RouteSegmentFactory.MakeDoublePoint("testvor", "testndb"),
                        RouteSegmentFactory.MakeDoublePoint("testndb", "testairport"),
                        RouteSegmentFactory.MakePointCoordinate("testairport", new Coordinate("abc", "def")),
                        RouteSegmentFactory.MakeCoordinatePoint("testfix", new Coordinate("abc", "def")),
                    }
                )
            );
            
            sectorElements.Fixes.Clear();
            AssertValidationErrors(2);
        }

        [Fact]
        public void TestItFailsOnBadVor()
        {
            sectorElements.Add(
                SidStarRouteFactory.Make(
                    segments: new List<RouteSegment>
                    {
                        RouteSegmentFactory.MakeDoublePoint("testfix", "testvor"),
                        RouteSegmentFactory.MakeDoublePoint("testvor", "testndb"),
                        RouteSegmentFactory.MakeDoublePoint("testndb", "testairport"),
                        RouteSegmentFactory.MakePointCoordinate("testairport", new Coordinate("abc", "def")),
                        RouteSegmentFactory.MakeCoordinatePoint("testvor", new Coordinate("abc", "def")),
                    }
                )
            );
            
            sectorElements.Vors.Clear();
            AssertValidationErrors(3);
        }

        [Fact]
        public void TestItFailsOnBadNdb()
        {
            sectorElements.Add(
                SidStarRouteFactory.Make(
                    segments: new List<RouteSegment>
                    {
                        RouteSegmentFactory.MakeDoublePoint("testfix", "testvor"),
                        RouteSegmentFactory.MakeDoublePoint("testvor", "testndb"),
                        RouteSegmentFactory.MakeDoublePoint("testndb", "testairport"),
                        RouteSegmentFactory.MakePointCoordinate("testairport", new Coordinate("abc", "def")),
                        RouteSegmentFactory.MakeCoordinatePoint("testvor", new Coordinate("abc", "def")),
                    }
                )
            );
            
            sectorElements.Ndbs.Clear();
            AssertValidationErrors(2);
        }

        [Fact]
        public void TestItFailsOnBadAirport()
        {
            sectorElements.Add(
                SidStarRouteFactory.Make(
                    segments: new List<RouteSegment>
                    {
                        RouteSegmentFactory.MakeDoublePoint("testfix", "testvor"),
                        RouteSegmentFactory.MakeDoublePoint("testvor", "testndb"),
                        RouteSegmentFactory.MakeDoublePoint("testndb", "testairport"),
                        RouteSegmentFactory.MakePointCoordinate("testairport", new Coordinate("abc", "def")),
                        RouteSegmentFactory.MakeCoordinatePoint("testvor", new Coordinate("abc", "def")),
                    }
                )
            );
            
            sectorElements.Airports.Clear();
            AssertValidationErrors(2);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSctSidsMustHaveAValidRoute();
        }
    }
}
