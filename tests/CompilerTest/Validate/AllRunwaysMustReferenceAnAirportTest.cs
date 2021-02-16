using Xunit;
using Moq;
using Compiler.Event;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllRunwaysMustReferenceAnAirportTest: AbstractValidatorTestCase
    {
        public AllRunwaysMustReferenceAnAirportTest()
        {
            loggerMock = new Mock<IEventLogger>();
            sectorElements.Add(AirportFactory.Make("EGKK"));
            sectorElements.Add(AirportFactory.Make("EGLL"));
            sectorElements.Add(AirportFactory.Make("000A"));
        }

        [Fact]
        public void TestItPassesOnValidReferences()
        {
            sectorElements.Add(RunwayFactory.Make("EGKK"));
            sectorElements.Add(RunwayFactory.Make("EGLL"));
            sectorElements.Add(RunwayFactory.Make("000A"));
            
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidReferences()
        {
            sectorElements.Add(RunwayFactory.Make("EGSS"));
            sectorElements.Add(RunwayFactory.Make("EGGW"));
            sectorElements.Add(RunwayFactory.Make("000A"));

            AssertValidationErrors(2);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllRunwaysMustReferenceAnAirport();
        }
    }
}
