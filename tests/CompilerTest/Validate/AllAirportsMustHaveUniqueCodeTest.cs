using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllAirportsMustHaveUniqueCodeTest: AbstractValidatorTestCase
    {
        private readonly Airport airfield1;
        private readonly Airport airfield2;
        private readonly Airport airfield3;

        public AllAirportsMustHaveUniqueCodeTest()
        {
            this.airfield1 = AirportFactory.Make("EGKK");
            this.airfield2 = AirportFactory.Make("EGLL");
            this.airfield3 = AirportFactory.Make("EGKK");
        }

        [Fact]
        public void TestItPassesOnNoDuplicates()
        {
            this.sectorElements.Add(airfield1);
            this.sectorElements.Add(airfield2);

            this.AssertNoValidationError();
        }

        [Fact]
        public void TestItFailsOnDuplicates()
        {
            this.sectorElements.Add(airfield1);
            this.sectorElements.Add(airfield2);
            this.sectorElements.Add(airfield3);

            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllAirportsMustHaveUniqueCode();
        }
    }
}
