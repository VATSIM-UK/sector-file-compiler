using Xunit;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllCoordinationPointsMustHaveValidPriorTest: AbstractValidatorTestCase
    {
        public AllCoordinationPointsMustHaveValidPriorTest()
        {
            sectorElements.Add(FixFactory.Make("testfix"));
            sectorElements.Add(VorFactory.Make("testvor"));
            sectorElements.Add(NdbFactory.Make("testndb"));
            sectorElements.Add(AirportFactory.Make("testairport"));
        }

        [Theory]
        [InlineData("testfix")]
        [InlineData("*")]
        [InlineData("testvor")]
        [InlineData("testndb")]
        [InlineData("EGGD")]
        public void TestItPassesOnValidPrior(string fix)
        {
            sectorElements.Add(CoordinationPointFactory.Make(priorPoint: fix));
            sectorElements.Add(CoordinationPointFactory.Make(priorPoint: fix));
            
            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("nottestfix","testfix")]
        [InlineData("testvor", "nottestvor")]
        [InlineData("nottestndb", "*")]
        [InlineData("EGGD", "EGG1")]
        public void TestItFailsOnInvalidPrior(string firstFix, string secondFix)
        {
            sectorElements.Add(CoordinationPointFactory.Make(priorPoint: firstFix));
            sectorElements.Add(CoordinationPointFactory.Make(priorPoint: secondFix));
            
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllCoordinationPointsMustHaveValidPrior();
        }
    }
}
