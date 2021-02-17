using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSidsMustHaveAValidAirportTest: AbstractValidatorTestCase
    {
        private readonly SidStar sid1;
        private readonly SidStar sid2;
        private readonly SidStar sid3;

        public AllSidsMustHaveAValidAirportTest()
        {
            sid1 = SidStarFactory.Make(airport: "EGKK");
            sid2 = SidStarFactory.Make(airport: "EGCC");
            sid3 = SidStarFactory.Make(airport: "EGBB");

            sectorElements.Add(AirportFactory.Make("EGKK"));
            sectorElements.Add(AirportFactory.Make("EGLL"));
            sectorElements.Add(AirportFactory.Make("EGCC"));
        }

        [Fact]
        public void TestItPassesOnAllValid()
        {
            sectorElements.Add(sid1);
            sectorElements.Add(sid2);

            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidAirport()
        {
            sectorElements.Add(sid1);
            sectorElements.Add(sid2);
            sectorElements.Add(sid3);

            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSidsMustHaveAValidAirport();
        }
    }
}
