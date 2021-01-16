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
            this.first = SidStarFactory.Make(route: new List<string>(new[] { "testfix", "testvor", "testndb", "testairport" }));
            this.second = SidStarFactory.Make(route: new List<string>(new[] { "nottestfix", "testvor", "testndb", "testairport" }));

            this.sectorElements.Add(FixFactory.Make("testfix"));
            this.sectorElements.Add(VorFactory.Make("testvor"));
            this.sectorElements.Add(NdbFactory.Make("testndb"));
            this.sectorElements.Add(AirportFactory.Make("testairport"));
        }

        [Fact]
        public void TestItPassesOnValidRoute()
        {
            this.sectorElements.Add(this.first);
            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidRoute()
        {
            this.sectorElements.Add(this.second);
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSidsMustHaveAValidRoute();
        }
    }
}
