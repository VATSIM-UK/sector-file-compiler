using Xunit;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllCoordinationPointsMustHaveValidFromSectorTest: AbstractValidatorTestCase
    {
        public AllCoordinationPointsMustHaveValidFromSectorTest()
        {
            this.sectorElements.Add(SectorFactory.Make("COOL1"));
            this.sectorElements.Add(SectorFactory.Make("COOL2"));
        }

        [Theory]
        [InlineData("COOL1")]
        [InlineData("COOL2")]
        public void TestItPassesOnValidToSector(string sector)
        {
            this.sectorElements.Add(CoordinationPointFactory.Make(fromSector: sector));
            this.sectorElements.Add(CoordinationPointFactory.Make(fromSector: sector));
            
            this.AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("COOL1","NOTCOOL1")]
        [InlineData("NOTCOOL2","COOL2")]
        public void TestItFailsOnInvalidToSector(string firstSector, string secondSector)
        {
            this.sectorElements.Add(CoordinationPointFactory.Make(fromSector: firstSector));
            this.sectorElements.Add(CoordinationPointFactory.Make(fromSector: secondSector));
            
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllCoordinationPointsMustHaveValidFromSector();
        }
    }
}
