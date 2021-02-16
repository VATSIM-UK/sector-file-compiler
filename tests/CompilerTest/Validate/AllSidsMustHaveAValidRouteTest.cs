using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSidsMustHaveAValidRouteTest: AbstractValidatorTestCase
    {
        private readonly SidStar first;
        private readonly SidStar second;

        public AllSidsMustHaveAValidRouteTest()
        {
            first = SidStarFactory.Make(route: new List<string>(new[] { "testfix", "testvor", "testndb", "testairport" }));
            second = SidStarFactory.Make(route: new List<string>(new[] { "nottestfix", "testvor", "testndb", "testairport" }));

            sectorElements.Add(FixFactory.Make("testfix"));
            sectorElements.Add(VorFactory.Make("testvor"));
            sectorElements.Add(NdbFactory.Make("testndb"));
            sectorElements.Add(AirportFactory.Make("testairport"));
        }

        [Fact]
        public void TestItPassesOnValidRoute()
        {
            sectorElements.Add(first);
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidRoute()
        {
            sectorElements.Add(second);
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSidsMustHaveAValidRoute();
        }
    }
}
