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
            airfield1 = AirportFactory.Make("EGKK");
            airfield2 = AirportFactory.Make("EGLL");
            airfield3 = AirportFactory.Make("EGKK");
        }

        [Fact]
        public void TestItPassesOnNoDuplicates()
        {
            sectorElements.Add(airfield1);
            sectorElements.Add(airfield2);

            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnDuplicates()
        {
            sectorElements.Add(airfield1);
            sectorElements.Add(airfield2);
            sectorElements.Add(airfield3);

            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllAirportsMustHaveUniqueCode();
        }
    }
}
