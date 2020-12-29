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
    public class AllGeoMustHaveValidColoursTest: AbstractValidatorTestCase
    {
        private readonly Colour definedColour;

        public AllGeoMustHaveValidColoursTest()
        {
            this.definedColour = ColourFactory.Make("colour1");
            this.sectorElements.Add(this.definedColour);
        }

        [Fact]
        public void TestItPassesOnValidColoursIntegers()
        {
            this.sectorElements.Add(GeoFactory.Make(
                "55",
                new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(colour: "42"),
                }
            ));

            this.AssertNoValidationError();
        }

        [Fact]
        public void TestItPassesOnValidDefinedColours()
        {
            this.sectorElements.Add(GeoFactory.Make(
                "colour1",
                new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(colour: "colour1"),
                }
            ));

            this.AssertNoValidationError();
        }

        [Fact]
        public void TestItFailsOnInvalidDefinedColours()
        {
            this.sectorElements.Add(GeoFactory.Make(
                "notcolour1",
                new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(colour: "notcolour1"),
                }
            ));

            this.AssertValidationErrors(2);
        }

        [Fact]
        public void TestItFailsOnInvalidDefinedColoursAfterLooping()
        {
            this.sectorElements.Add(GeoFactory.Make(
                "colour1",
                new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(colour: "colour1"),
                }
            ));
            
            this.sectorElements.Add(GeoFactory.Make(
                "notcolour1",
                new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(colour: "notcolour1"),
                }
            ));

            this.AssertValidationErrors(2);
        }

        [Fact]
        public void TestItFailsOnInvalidColourIntegers()
        {
            this.sectorElements.Add(GeoFactory.Make(
                "123456789",
                new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(colour: "123456789"),
                }
            ));
            
            this.AssertValidationErrors(2);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllGeoMustHaveValidColours();
        }
    }
}
