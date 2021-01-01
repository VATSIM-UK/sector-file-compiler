using Xunit;
using System.Collections.Generic;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Validate;
using Moq;
using Compiler.Argument;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSctStarsMustHaveValidColoursTest: AbstractValidatorTestCase
    {
        public AllSctStarsMustHaveValidColoursTest()
        {
            this.sectorElements.Add(ColourFactory.Make("colour1"));
        }

        [Fact]
        public void TestItPassesOnValidColoursIntegers()
        {
            this.sectorElements.Add(
                SidStarRouteFactory.Make(
                    SidStarType.STAR,
                    new List<RouteSegment>()
                    {
                        RouteSegmentFactory.MakeDoublePoint(colour: "255"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "266"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "266"),
                    }
                )
            );
            
            this.AssertNoValidationError();
        }

        [Fact]
        public void TestItPassesOnValidDefinedColours()
        {
            this.sectorElements.Add(
                SidStarRouteFactory.Make(
                    SidStarType.STAR,
                    new List<RouteSegment>()
                    {
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour1"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour1"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour1"),
                    }
                )
            );
            
            this.AssertNoValidationError();
        }

        [Fact]
        public void TestItPassesOnNullColours()
        {
            this.sectorElements.Add(
                SidStarRouteFactory.Make(
                    SidStarType.STAR,
                    new List<RouteSegment>()
                    {
                        RouteSegmentFactory.MakeDoublePointWithNoColour(),
                        RouteSegmentFactory.MakeDoublePointWithNoColour(),
                        RouteSegmentFactory.MakeDoublePointWithNoColour(),
                    }
                )
            );
            
            this.AssertNoValidationError();
        }

        [Fact]
        public void TestItFailsOnInvalidDefinedColours()
        {
            this.sectorElements.Add(
                SidStarRouteFactory.Make(
                    SidStarType.STAR,
                    new List<RouteSegment>()
                    {
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour1"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour2"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour1"),
                    }
                )
            );
            
            this.AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidDefinedColoursAfterLooping()
        {
            this.sectorElements.Add(
                SidStarRouteFactory.Make(
                    SidStarType.STAR,
                    new List<RouteSegment>()
                    {
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour1"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour1"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "colour2"),
                    }
                )
            );
            
            this.AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidColourIntegers()
        {
            this.sectorElements.Add(
                SidStarRouteFactory.Make(
                    SidStarType.STAR,
                    new List<RouteSegment>()
                    {
                        RouteSegmentFactory.MakeDoublePoint(colour: "255"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "-1"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "255"),
                    }
                )
            );
            
            this.AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidColourIntegersAfterLooping()
        {
            this.sectorElements.Add(
                SidStarRouteFactory.Make(
                    SidStarType.STAR,
                    new List<RouteSegment>()
                    {
                        RouteSegmentFactory.MakeDoublePoint(colour: "255"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "255"),
                        RouteSegmentFactory.MakeDoublePoint(colour: "-1"),
                    }
                )
            );
            
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSctStarsMustHaveValidColours();
        }
    }
}
