using Xunit;
using Moq;
using Compiler.Model;
using Compiler.Validate;
using Compiler.Event;
using Compiler.Error;
using Compiler.Argument;
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
