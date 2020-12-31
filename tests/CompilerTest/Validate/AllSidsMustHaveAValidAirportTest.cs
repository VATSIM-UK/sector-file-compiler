using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Model;
using Compiler.Validate;
using Compiler.Event;
using Compiler.Error;
using Compiler.Argument;
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
            this.sid1 = SidStarFactory.Make(airport: "EGKK");
            this.sid2 = SidStarFactory.Make(airport: "EGCC");
            this.sid3 = SidStarFactory.Make(airport: "EGBB");

            this.sectorElements.Add(AirportFactory.Make("EGKK"));
            this.sectorElements.Add(AirportFactory.Make("EGLL"));
            this.sectorElements.Add(AirportFactory.Make("EGCC"));
        }

        [Fact]
        public void TestItPassesOnAllValid()
        {
            this.sectorElements.Add(sid1);
            this.sectorElements.Add(sid2);

            this.AssertNoValidationError();
        }

        [Fact]
        public void TestItFailsOnInvalidAirport()
        {
            this.sectorElements.Add(sid1);
            this.sectorElements.Add(sid2);
            this.sectorElements.Add(sid3);

            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSidsMustHaveAValidAirport();
        }
    }
}
