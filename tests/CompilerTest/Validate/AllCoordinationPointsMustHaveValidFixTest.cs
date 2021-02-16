using Xunit;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllCoordinationPointsMustHaveValidFixTest: AbstractValidatorTestCase
    {
        public AllCoordinationPointsMustHaveValidFixTest()
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
        [InlineData("testairport")]
        public void TestItPassesOnValidFix(string fix)
        {
            sectorElements.Add(CoordinationPointFactory.Make(coordinationPoint: fix));
            sectorElements.Add(CoordinationPointFactory.Make(coordinationPoint: fix));
            
            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("nottestfix","testfix")]
        [InlineData("testvor", "nottestvor")]
        [InlineData("nottestndb", "*")]
        [InlineData("testairport", "nottestairport")]
        public void TestItFailsOnInvalidFix(string firstFix, string secondFix)
        {
            sectorElements.Add(CoordinationPointFactory.Make(coordinationPoint: firstFix));
            sectorElements.Add(CoordinationPointFactory.Make(coordinationPoint: secondFix));
            
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllCoordinationPointsMustHaveValidFix();
        }
    }
}
