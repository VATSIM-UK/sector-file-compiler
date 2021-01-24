using System.Collections.Generic;
using Compiler.Injector;
using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class CentrelineColourIsDefinedTest: AbstractValidatorTestCase
    {
        public CentrelineColourIsDefinedTest()
        {
            RunwayCentrelineInjector.InjectRunwayCentrelineData(sectorElements);
        }

        [Fact]
        public void TestItPassesOnColourDefined()
        {
            sectorElements.Add(ColourFactory.Make("centrelinecolour"));
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsIfColourNotDefined()
        {
            sectorElements.Add(ColourFactory.Make("notcentrelinecolour"));
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new CentrelineColourIsDefined();
        }
    }
}
