using Xunit;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllCoordinationPointsMustHaveValidNextTest: AbstractValidatorTestCase
    {
        public AllCoordinationPointsMustHaveValidNextTest()
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
        public void TestItPassesOnValidNext(string fix)
        {
            this.sectorElements.Add(CoordinationPointFactory.Make(nextPoint: fix));
            this.sectorElements.Add(CoordinationPointFactory.Make(nextPoint: fix));
            
            this.AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("nottestfix","testfix")]
        [InlineData("testvor", "nottestvor")]
        [InlineData("nottestndb", "*")]
        [InlineData("EGGD", "EGG1")]
        public void TestItFailsOnInvalidNext(string firstFix, string secondFix)
        {
            this.sectorElements.Add(CoordinationPointFactory.Make(nextPoint: firstFix));
            this.sectorElements.Add(CoordinationPointFactory.Make(nextPoint: secondFix));
            
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllCoordinationPointsMustHaveValidNext();
        }
    }
}
