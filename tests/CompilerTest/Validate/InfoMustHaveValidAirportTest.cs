using Xunit;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class InfoMustHaveValidAirportTest: AbstractValidatorTestCase
    {
        public InfoMustHaveValidAirportTest()
        {
            this.sectorElements.Add(AirportFactory.Make("EGLL"));
            this.sectorElements.Add(AirportFactory.Make("EGKK"));
        }

        [Fact]
        public void TestItPassesOnValidAirport()
        {
            this.sectorElements.Add(InfoFactory.Make("EGLL"));
            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidAirport()
        {
            this.sectorElements.Add(InfoFactory.Make("EGXX"));
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new InfoMustHaveValidAirport();
        }
    }
}
