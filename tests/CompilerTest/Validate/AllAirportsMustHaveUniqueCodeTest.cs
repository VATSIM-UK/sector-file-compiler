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
            this.elements = new SectorElementCollection();
            this.airfield1 = AirportFactory.Make("EGKK");
            this.airfield2 = AirportFactory.Make("EGLL");
            this.airfield3 = AirportFactory.Make("EGKK");
        }

        [Fact]
        public void TestItPassesOnNoDuplicates()
        {
            this.elements.Add(airfield1);
            this.elements.Add(airfield2);

            this.AssertNoValidationError();
        }

        [Fact]
        public void TestItFailsOnDuplicates()
        {
            this.elements.Add(airfield1);
            this.elements.Add(airfield2);
            this.elements.Add(airfield3);

            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllAirportsMustHaveUniqueCode();
        }
    }
}
