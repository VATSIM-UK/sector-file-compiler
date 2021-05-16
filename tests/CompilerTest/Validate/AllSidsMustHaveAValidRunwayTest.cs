using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSidsMustHaveAValidRunwayTest: AbstractValidatorTestCase
    {
        private readonly SidStar sid1;
        private readonly SidStar sid2;
        private readonly SidStar sid3;
        private readonly SidStar sid4;

        public AllSidsMustHaveAValidRunwayTest()
        {
            sid1 = SidStarFactory.Make(airport: "EGKK", runway: "26L");
            sid2 = SidStarFactory.Make(airport: "EGCC", runway: "23R");
            sid3 = SidStarFactory.Make(airport: "EGBB", runway: "16");
            sid4 = SidStarFactory.Make(airport: "EGKB", runway: "08");

            sectorElements.Add(AirportFactory.Make("EGKK"));
            sectorElements.Add(AirportFactory.Make("EGCC"));
            sectorElements.Add(AirportFactory.Make("EGBB"));
            sectorElements.Add(RunwayFactory.Make("EGKK", "26L", "08R"));
            sectorElements.Add(RunwayFactory.Make("EGCC", "23R", "05L"));
            sectorElements.Add(RunwayFactory.Make("EGBB", "33", "15"));
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
            sectorElements.Add(sid3);  // Airport exists, not runway
            sectorElements.Add(sid4);  // Airport doesn't exist

            AssertValidationErrors(2);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSidsMustHaveAValidRunway();
        }
    }
}
