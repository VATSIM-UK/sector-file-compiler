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
    public class AllCoordinationPointsMustHaveValidFixTest: AbstractValidatorTestCase
    {
        public AllCoordinationPointsMustHaveValidFixTest()
        {
            this.sectorElements.Add(FixFactory.Make("testfix"));
            this.sectorElements.Add(VorFactory.Make("testvor"));
            this.sectorElements.Add(NdbFactory.Make("testndb"));
            this.sectorElements.Add(AirportFactory.Make("testairport"));
        }

        [Theory]
        [InlineData("testfix")]
        [InlineData("*")]
        [InlineData("testvor")]
        [InlineData("testndb")]
        [InlineData("testairport")]
        public void TestItPassesOnValidFix(string fix)
        {
            this.sectorElements.Add(CoordinationPointFactory.Make(coordinationPoint: fix));
            this.sectorElements.Add(CoordinationPointFactory.Make(coordinationPoint: fix));
            
            this.AssertNoValidationError();
        }

        [Theory]
        [InlineData("nottestfix","testfix")]
        [InlineData("testvor", "nottestvor")]
        [InlineData("nottestndb", "*")]
        [InlineData("testairport", "nottestairport")]
        public void TestItFailsOnInvalidFix(string firstFix, string secondFix)
        {
            this.sectorElements.Add(CoordinationPointFactory.Make(coordinationPoint: firstFix));
            this.sectorElements.Add(CoordinationPointFactory.Make(coordinationPoint: secondFix));
            
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllCoordinationPointsMustHaveValidFix();
        }
    }
}
