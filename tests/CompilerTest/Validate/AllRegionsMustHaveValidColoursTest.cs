using Xunit;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Validate;
using Moq;
using Compiler.Argument;
using System.Collections.Generic;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllRegionsMustHaveAValidColourTest: AbstractValidatorTestCase
    {
        public AllRegionsMustHaveAValidColourTest()
        {
            this.sectorElements.Add(ColourFactory.Make("colour1"));
        }

        [Fact]
        public void TestItPassesOnValidColours()
        {
            this.sectorElements.Add(RegionFactory.Make("colour1"));
            this.sectorElements.Add(RegionFactory.Make("123"));

            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidColours()
        {
            this.sectorElements.Add(RegionFactory.Make("colour2"));
            this.sectorElements.Add(RegionFactory.Make("-123"));

            this.AssertValidationErrors(4);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllRegionsMustHaveValidColours();
        }
    }
}
