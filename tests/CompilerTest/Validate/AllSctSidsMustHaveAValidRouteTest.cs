using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;
using Compiler.Argument;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSctSidsMustHaveAValidRouteTest: AbstractValidatorTestCase
    {
        public AllSctSidsMustHaveAValidRouteTest()
        {
            this.sectorElements.Add(FixFactory.Make("testfix"));
            this.sectorElements.Add(VorFactory.Make("testvor"));
            this.sectorElements.Add(NdbFactory.Make("testndb"));
            this.sectorElements.Add(AirportFactory.Make("testairport"));
        }

        [Fact]
        public void TestItPassesOnValidRoute()
        {
            this.sectorElements.Add(
                SidStarRouteFactory.Make(
                    segments: new List<RouteSegment>()
                    {
                        RouteSegmentFactory.MakeDoublePoint("testfix", "testvor"),
                        RouteSegmentFactory.MakeDoublePoint("testvor", "testndb"),
                        RouteSegmentFactory.MakeDoublePoint("testndb", "testairport"),
                        RouteSegmentFactory.MakePointCoordinate("testairport", new Coordinate("abc", "def")),
                        RouteSegmentFactory.MakeCoordinatePoint("testvor", new Coordinate("abc", "def")),
                    }
                )
            );
           
            this.AssertNoValidationError();
        }

        [Fact]
        public void TestItFailsOnBadFix()
        {
            this.sectorElements.Add(
                SidStarRouteFactory.Make(
                    segments: new List<RouteSegment>()
                    {
                        RouteSegmentFactory.MakeDoublePoint("testfix", "testvor"),
                        RouteSegmentFactory.MakeDoublePoint("testvor", "testndb"),
                        RouteSegmentFactory.MakeDoublePoint("testndb", "testairport"),
                        RouteSegmentFactory.MakePointCoordinate("testairport", new Coordinate("abc", "def")),
                        RouteSegmentFactory.MakeCoordinatePoint("testvor", new Coordinate("abc", "def")),
                    }
                )
            );
            
            this.sectorElements.Fixes.Clear();
            this.AssertValidationErrors(2);
        }

        [Fact]
        public void TestItFailsOnBadVor()
        {
            this.sectorElements.Add(
                SidStarRouteFactory.Make(
                    segments: new List<RouteSegment>()
                    {
                        RouteSegmentFactory.MakeDoublePoint("testfix", "testvor"),
                        RouteSegmentFactory.MakeDoublePoint("testvor", "testndb"),
                        RouteSegmentFactory.MakeDoublePoint("testndb", "testairport"),
                        RouteSegmentFactory.MakePointCoordinate("testairport", new Coordinate("abc", "def")),
                        RouteSegmentFactory.MakeCoordinatePoint("testvor", new Coordinate("abc", "def")),
                    }
                )
            );
            
            this.sectorElements.Vors.Clear();
            this.AssertValidationErrors(2);
        }

        [Fact]
        public void TestItFailsOnBadNdb()
        {
            this.sectorElements.Add(
                SidStarRouteFactory.Make(
                    segments: new List<RouteSegment>()
                    {
                        RouteSegmentFactory.MakeDoublePoint("testfix", "testvor"),
                        RouteSegmentFactory.MakeDoublePoint("testvor", "testndb"),
                        RouteSegmentFactory.MakeDoublePoint("testndb", "testairport"),
                        RouteSegmentFactory.MakePointCoordinate("testairport", new Coordinate("abc", "def")),
                        RouteSegmentFactory.MakeCoordinatePoint("testvor", new Coordinate("abc", "def")),
                    }
                )
            );
            
            this.sectorElements.Ndbs.Clear();
            this.AssertValidationErrors(2);
        }

        [Fact]
        public void TestItFailsOnBadAirport()
        {
            this.sectorElements.Add(
                SidStarRouteFactory.Make(
                    segments: new List<RouteSegment>()
                    {
                        RouteSegmentFactory.MakeDoublePoint("testfix", "testvor"),
                        RouteSegmentFactory.MakeDoublePoint("testvor", "testndb"),
                        RouteSegmentFactory.MakeDoublePoint("testndb", "testairport"),
                        RouteSegmentFactory.MakePointCoordinate("testairport", new Coordinate("abc", "def")),
                        RouteSegmentFactory.MakeCoordinatePoint("testvor", new Coordinate("abc", "def")),
                    }
                )
            );
            
            this.sectorElements.Airports.Clear();
            this.AssertValidationErrors(2);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSctSidsMustHaveAValidRoute();
        }
    }
}
