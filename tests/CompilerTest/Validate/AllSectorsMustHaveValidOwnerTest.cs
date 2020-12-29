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
    public class AllSectorsMustHaveValidOwnerTest: AbstractValidatorTestCase
    {
        public AllSectorsMustHaveValidOwnerTest()
        {
            this.sectorElements.Add(ControllerPositionFactory.Make("BBR"));
            this.sectorElements.Add(ControllerPositionFactory.Make("BBF"));
            this.sectorElements.Add(ControllerPositionFactory.Make("LS"));
            this.sectorElements.Add(ControllerPositionFactory.Make("LC"));
        }

        [Fact]
        public void TestItPassesOnValidControllers()
        {
            this.sectorElements.Add(
                SectorFactory.Make(
                    owners: SectorOwnerHierarchyFactory.Make(new List<string>(){"LS", "LC"})
                )
            );
            this.sectorElements.Add(
                SectorFactory.Make(
                    owners: SectorOwnerHierarchyFactory.Make(new List<string>(){"LS", "LC", "BBR", "BBF"})
                )
            );
            
            this.AssertNoValidationError();
        }

        [Theory]
        [InlineData("LS","WHAT")]
        [InlineData("LC", "WHAT")]
        [InlineData("BBR", "WHAT")]
        [InlineData("BBF","WHAT")]
        public void TestItFailsOnInvalidControllers(string first, string second)
        {
            this.sectorElements.Add(
                SectorFactory.Make(
                    owners: SectorOwnerHierarchyFactory.Make(new List<string>(){first})
                )
            );
            this.sectorElements.Add(
                SectorFactory.Make(
                    owners: SectorOwnerHierarchyFactory.Make(new List<string>(){first, second})
                )
            );
            
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSectorsMustHaveValidOwner();
        }
    }
}
