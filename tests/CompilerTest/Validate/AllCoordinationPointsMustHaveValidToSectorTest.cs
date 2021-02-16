using Xunit;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllCoordinationPointsMustHaveValidToSectorTest: AbstractValidatorTestCase
    {
        public AllCoordinationPointsMustHaveValidToSectorTest()
        {
            sectorElements.Add(SectorFactory.Make("COOL1"));
            sectorElements.Add(SectorFactory.Make("COOL2"));
        }

        [Theory]
        [InlineData("COOL1")]
        [InlineData("COOL2")]
        public void TestItPassesOnValidToSector(string sector)
        {
            sectorElements.Add(CoordinationPointFactory.Make(toSector: sector));
            sectorElements.Add(CoordinationPointFactory.Make(toSector: sector));
            
            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("COOL1","NOTCOOL1")]
        [InlineData("NOTCOOL2","COOL2")]
        public void TestItFailsOnInvalidToSector(string firstSector, string secondSector)
        {
            sectorElements.Add(CoordinationPointFactory.Make(toSector: firstSector));
            sectorElements.Add(CoordinationPointFactory.Make(toSector: secondSector));
            
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllCoordinationPointsMustHaveValidToSector();
        }
    }
}
