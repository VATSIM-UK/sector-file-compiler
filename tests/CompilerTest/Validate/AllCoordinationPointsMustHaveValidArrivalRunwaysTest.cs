using Xunit;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllCoordinationPointsMustHaveValidArrivalRunwaysTest: AbstractValidatorTestCase
    {
        public AllCoordinationPointsMustHaveValidArrivalRunwaysTest()
        {
            sectorElements.Add(AirportFactory.Make("EGKK"));
            sectorElements.Add(RunwayFactory.Make("EGKK", "26L", "09"));
            sectorElements.Add(AirportFactory.Make("EGLL"));
            sectorElements.Add(RunwayFactory.Make("EGLL", "09R", "09"));
            sectorElements.Add(RunwayFactory.Make("EGLL", "09L", "09"));
            sectorElements.Add(AirportFactory.Make("EGSS"));
        }

        [Theory]
        [InlineData("EGKK", "26L")]
        [InlineData("EGLL", "09R")]
        [InlineData("EGLL", "09L")]
        [InlineData("EGLL", "*")]
        [InlineData("EGSS", "*")]
        [InlineData("XXXX", "*")] // Airport doesnt exist, but any runway
        [InlineData("*", "09R")] // Should never happen
        [InlineData("*", "*")]

        public void TestItPassesOnValidArrivalRunway(string airport, string runway)
        {
            sectorElements.Add(CoordinationPointFactory.MakeAirport(arrivalAirport: airport, arrivalRunway: runway));
            
            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("EGLL", "27R")]
        [InlineData("EGLL", "27L")]
        [InlineData("EGKK", "08R")]
        [InlineData("EGKK", "08L")]
        [InlineData("EGSS", "04")] // Doesn't match up on the descriptions
        public void TestItFailsOnInvalidArrivalRunway(string airport, string runway)
        {
            sectorElements.Add(CoordinationPointFactory.MakeAirport(arrivalAirport: airport, arrivalRunway: runway));
            
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllCoordinationPointsMustHaveValidArrivalRunways();
        }
    }
}
