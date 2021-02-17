using Xunit;
using System.Collections.Generic;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllGeoMustHaveValidColoursTest: AbstractValidatorTestCase
    {
        public AllGeoMustHaveValidColoursTest()
        {
            sectorElements.Add(ColourFactory.Make("colour1"));
        }

        [Fact]
        public void TestItPassesOnValidColoursIntegers()
        {
            sectorElements.Add(GeoFactory.Make(
                "55",
                new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(colour: "42"),
                }
            ));

            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItPassesOnValidDefinedColours()
        {
            sectorElements.Add(GeoFactory.Make(
                "colour1",
                new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(colour: "colour1"),
                }
            ));

            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidDefinedColours()
        {
            sectorElements.Add(GeoFactory.Make(
                "notcolour1",
                new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(colour: "notcolour1"),
                }
            ));

            AssertValidationErrors(2);
        }

        [Fact]
        public void TestItFailsOnInvalidDefinedColoursAfterLooping()
        {
            sectorElements.Add(GeoFactory.Make(
                "colour1",
                new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(colour: "colour1"),
                }
            ));
            
            sectorElements.Add(GeoFactory.Make(
                "notcolour1",
                new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(colour: "notcolour1"),
                }
            ));

            AssertValidationErrors(2);
        }

        [Fact]
        public void TestItFailsOnInvalidColourIntegers()
        {
            sectorElements.Add(GeoFactory.Make(
                "123456789",
                new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(colour: "123456789"),
                }
            ));
            
            AssertValidationErrors(2);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllGeoMustHaveValidColours();
        }
    }
}
