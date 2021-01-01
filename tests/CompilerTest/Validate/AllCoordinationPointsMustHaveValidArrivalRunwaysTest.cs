using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;
using Compiler.Argument;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllCoordinationPointsMustHaveValidArrivalRunwaysTest: AbstractValidatorTestCase
    {
        public AllCoordinationPointsMustHaveValidArrivalRunwaysTest()
        {
            this.sectorElements.Add(AirportFactory.Make("EGKK"));
            this.sectorElements.Add(RunwayFactory.Make("EGKK", "26L", "09"));
            this.sectorElements.Add(AirportFactory.Make("EGLL"));
            this.sectorElements.Add(RunwayFactory.Make("EGLL", "09R", "09"));
            this.sectorElements.Add(RunwayFactory.Make("EGLL", "09L", "09"));
            this.sectorElements.Add(AirportFactory.Make("EGSS"));
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
            this.sectorElements.Add(CoordinationPointFactory.MakeAirport(arrivalAirport: airport, arrivalRunway: runway));
            
            this.AssertNoValidationError();
        }

        [Theory]
        [InlineData("EGLL", "27R")]
        [InlineData("EGLL", "27L")]
        [InlineData("EGKK", "08R")]
        [InlineData("EGKK", "08L")]
        [InlineData("EGSS", "04")] // Doesn't match up on the descriptions
        public void TestItFailsOnInvalidArrivalRunway(string airport, string runway)
        {
            this.sectorElements.Add(CoordinationPointFactory.MakeAirport(arrivalAirport: airport, arrivalRunway: runway));
            
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllCoordinationPointsMustHaveValidArrivalRunways();
        }
    }
}
