using Xunit;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class InfoMustHaveValidAirportTest: AbstractValidatorTestCase
    {
        public InfoMustHaveValidAirportTest()
        {
            sectorElements.Add(AirportFactory.Make("EGLL"));
            sectorElements.Add(AirportFactory.Make("EGKK"));
        }

        [Fact]
        public void TestItPassesOnValidAirport()
        {
            sectorElements.Add(InfoFactory.Make("EGLL"));
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidAirport()
        {
            sectorElements.Add(InfoFactory.Make("EGXX"));
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new InfoMustHaveValidAirport();
        }
    }
}
