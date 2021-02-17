using Xunit;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllCoordinationPointsMustHaveValidFromSectorTest: AbstractValidatorTestCase
    {
        public AllCoordinationPointsMustHaveValidFromSectorTest()
        {
            sectorElements.Add(SectorFactory.Make("COOL1"));
            sectorElements.Add(SectorFactory.Make("COOL2"));
        }

        [Theory]
        [InlineData("COOL1")]
        [InlineData("COOL2")]
        public void TestItPassesOnValidToSector(string sector)
        {
            sectorElements.Add(CoordinationPointFactory.Make(fromSector: sector));
            sectorElements.Add(CoordinationPointFactory.Make(fromSector: sector));
            
            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("COOL1","NOTCOOL1")]
        [InlineData("NOTCOOL2","COOL2")]
        public void TestItFailsOnInvalidToSector(string firstSector, string secondSector)
        {
            sectorElements.Add(CoordinationPointFactory.Make(fromSector: firstSector));
            sectorElements.Add(CoordinationPointFactory.Make(fromSector: secondSector));
            
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllCoordinationPointsMustHaveValidFromSector();
        }
    }
}
