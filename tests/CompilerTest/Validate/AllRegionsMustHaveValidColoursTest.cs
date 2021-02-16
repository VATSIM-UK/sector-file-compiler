using Xunit;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllRegionsMustHaveAValidColourTest: AbstractValidatorTestCase
    {
        public AllRegionsMustHaveAValidColourTest()
        {
            sectorElements.Add(ColourFactory.Make("colour1"));
        }

        [Fact]
        public void TestItPassesOnValidColours()
        {
            sectorElements.Add(RegionFactory.Make("colour1"));
            sectorElements.Add(RegionFactory.Make("123"));

            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidColours()
        {
            sectorElements.Add(RegionFactory.Make("colour2"));
            sectorElements.Add(RegionFactory.Make("-123"));

            AssertValidationErrors(4);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllRegionsMustHaveValidColours();
        }
    }
}
