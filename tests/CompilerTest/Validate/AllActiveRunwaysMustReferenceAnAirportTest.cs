using Compiler.Validate;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Validate
{
    public class AllActiveRunwaysMustReferenceAnAirportTest: AbstractValidatorTestCase
    {
        public AllActiveRunwaysMustReferenceAnAirportTest()
        {
            sectorElements.Add(AirportFactory.Make("EGLL"));
            sectorElements.Add(AirportFactory.Make("EGKK"));
            sectorElements.Add(AirportFactory.Make("EGCC"));
        }
        
        [Fact]
        public void TestItFailsIfAirportDoesntExist()
        {
            sectorElements.Add(ActiveRunwayFactory.Make("EGKB"));
            sectorElements.Add(ActiveRunwayFactory.Make("EGLC"));
            sectorElements.Add(ActiveRunwayFactory.Make("EGGD"));
            AssertValidationErrors(3);
        }
        
        [Fact]
        public void TestItPassesIfAirportExists()
        {
            sectorElements.Add(ActiveRunwayFactory.Make("EGLL"));
            sectorElements.Add(ActiveRunwayFactory.Make("EGKK"));
            sectorElements.Add(ActiveRunwayFactory.Make("EGCC"));
            AssertNoValidationErrors();
        }
        
        protected override IValidationRule GetValidationRule()
        {
            return new AllActiveRunwaysMustReferenceAnAirport();
        }
    }
}