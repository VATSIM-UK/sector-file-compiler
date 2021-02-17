using Xunit;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllCoordinationPointsMustHaveValidNextTest: AbstractValidatorTestCase
    {
        public AllCoordinationPointsMustHaveValidNextTest()
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
        public void TestItPassesOnValidNext(string fix)
        {
            sectorElements.Add(CoordinationPointFactory.Make(nextPoint: fix));
            sectorElements.Add(CoordinationPointFactory.Make(nextPoint: fix));
            
            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("nottestfix","testfix")]
        [InlineData("testvor", "nottestvor")]
        [InlineData("nottestndb", "*")]
        [InlineData("EGGD", "EGG1")]
        public void TestItFailsOnInvalidNext(string firstFix, string secondFix)
        {
            sectorElements.Add(CoordinationPointFactory.Make(nextPoint: firstFix));
            sectorElements.Add(CoordinationPointFactory.Make(nextPoint: secondFix));
            
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllCoordinationPointsMustHaveValidNext();
        }
    }
}
