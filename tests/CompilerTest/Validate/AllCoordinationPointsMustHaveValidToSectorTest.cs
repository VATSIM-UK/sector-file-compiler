using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;
using Compiler.Argument;
using System.Collections.Generic;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllCoordinationPointsMustHaveValidToSectorTest: AbstractValidatorTestCase
    {
        public AllCoordinationPointsMustHaveValidToSectorTest()
        {
            this.sectorElements.Add(SectorFactory.Make("COOL1"));
            this.sectorElements.Add(SectorFactory.Make("COOL2"));
        }

        [Theory]
        [InlineData("COOL1")]
        [InlineData("COOL2")]
        public void TestItPassesOnValidToSector(string sector)
        {
            this.sectorElements.Add(CoordinationPointFactory.Make(toSector: sector));
            this.sectorElements.Add(CoordinationPointFactory.Make(toSector: sector));
            
            this.AssertNoValidationError();
        }

        [Theory]
        [InlineData("COOL1","NOTCOOL1")]
        [InlineData("NOTCOOL2","COOL2")]
        public void TestItFailsOnInvalidToSector(string firstSector, string secondSector)
        {
            this.sectorElements.Add(CoordinationPointFactory.Make(toSector: firstSector));
            this.sectorElements.Add(CoordinationPointFactory.Make(toSector: secondSector));
            
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllCoordinationPointsMustHaveValidToSector();
        }
    }
}
