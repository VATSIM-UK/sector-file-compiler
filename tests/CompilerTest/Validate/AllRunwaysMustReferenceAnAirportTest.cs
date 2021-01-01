using Xunit;
using Moq;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Argument;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllRunwaysMustReferenceAnAirportTest: AbstractValidatorTestCase
    {
        public AllRunwaysMustReferenceAnAirportTest()
        {
            this.loggerMock = new Mock<IEventLogger>();
            this.sectorElements.Add(AirportFactory.Make("EGKK"));
            this.sectorElements.Add(AirportFactory.Make("EGLL"));
            this.sectorElements.Add(AirportFactory.Make("000A"));
        }

        [Fact]
        public void TestItPassesOnValidReferences()
        {
            this.sectorElements.Add(RunwayFactory.Make("EGKK"));
            this.sectorElements.Add(RunwayFactory.Make("EGLL"));
            this.sectorElements.Add(RunwayFactory.Make("000A"));
            
            this.AssertNoValidationError();
        }

        [Fact]
        public void TestItFailsOnInvalidReferences()
        {
            this.sectorElements.Add(RunwayFactory.Make("EGSS"));
            this.sectorElements.Add(RunwayFactory.Make("EGGW"));
            this.sectorElements.Add(RunwayFactory.Make("000A"));

            this.AssertValidationErrors(2);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllRunwaysMustReferenceAnAirport();
        }
    }
}
