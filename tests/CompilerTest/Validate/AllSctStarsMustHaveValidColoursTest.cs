using Xunit;
using System.Collections.Generic;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSctStarsMustHaveValidColoursTest: AbstractValidatorTestCase
    {
        public AllSctStarsMustHaveValidColoursTest()
        {
            sectorElements.Add(ColourFactory.Make("colour1"));
        }

        [Fact]
        public void TestItPassesOnValidColoursIntegers()
        {
            sectorElements.Add(
                SidStarRouteFactory.Make(
                    SidStarType.STAR,
                    new List<RouteSegment>
                    {
                        RouteSegmentFactory.MakeDoublePoint(colour: "255"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "266"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "266"),
                    }
                )
            );
            
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItPassesOnValidDefinedColours()
        {
            sectorElements.Add(
                SidStarRouteFactory.Make(
                    SidStarType.STAR,
                    new List<RouteSegment>
                    {
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour1"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour1"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour1"),
                    }
                )
            );
            
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItPassesOnNullColours()
        {
            sectorElements.Add(
                SidStarRouteFactory.Make(
                    SidStarType.STAR,
                    new List<RouteSegment>
                    {
                        RouteSegmentFactory.MakeDoublePointWithNoColour(),
                        RouteSegmentFactory.MakeDoublePointWithNoColour(),
                        RouteSegmentFactory.MakeDoublePointWithNoColour(),
                    }
                )
            );
            
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidDefinedColours()
        {
            sectorElements.Add(
                SidStarRouteFactory.Make(
                    SidStarType.STAR,
                    new List<RouteSegment>
                    {
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour1"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour2"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour1"),
                    }
                )
            );
            
            AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidDefinedColoursAfterLooping()
        {
            sectorElements.Add(
                SidStarRouteFactory.Make(
                    SidStarType.STAR,
                    new List<RouteSegment>
                    {
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour1"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour1"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour2"),
                    }
                )
            );
            
            AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidColourIntegers()
        {
            sectorElements.Add(
                SidStarRouteFactory.Make(
                    SidStarType.STAR,
                    new List<RouteSegment>
                    {
                        RouteSegmentFactory.MakeDoublePoint(colour: "255"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "-1"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "255"),
                    }
                )
            );
            
            AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidColourIntegersAfterLooping()
        {
            sectorElements.Add(
                SidStarRouteFactory.Make(
                    SidStarType.STAR,
                    new List<RouteSegment>
                    {
                        RouteSegmentFactory.MakeDoublePoint(colour: "255"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "255"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "-1"),
                    }
                )
            );
            
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSctStarsMustHaveValidColours();
        }
    }
}
