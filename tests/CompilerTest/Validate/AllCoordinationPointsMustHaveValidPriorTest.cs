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
    public class AllCoordinationPointsMustHaveValidPriorTest: AbstractValidatorTestCase
    {
        public AllCoordinationPointsMustHaveValidPriorTest()
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
        [InlineData("EGGD")]
        public void TestItPassesOnValidPrior(string fix)
        {
            this.sectorElements.Add(CoordinationPointFactory.Make(priorPoint: fix));
            this.sectorElements.Add(CoordinationPointFactory.Make(priorPoint: fix));
            
            this.AssertNoValidationError();
        }

        [Theory]
        [InlineData("nottestfix","testfix")]
        [InlineData("testvor", "nottestvor")]
        [InlineData("nottestndb", "*")]
        [InlineData("EGGD", "EGG1")]
        public void TestItFailsOnInvalidPrior(string firstFix, string secondFix)
        {
            this.sectorElements.Add(CoordinationPointFactory.Make(priorPoint: firstFix));
            this.sectorElements.Add(CoordinationPointFactory.Make(priorPoint: secondFix));
            
            this.AssertNoValidationError();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllCoordinationPointsMustHaveValidPrior();
        }
    }
}
