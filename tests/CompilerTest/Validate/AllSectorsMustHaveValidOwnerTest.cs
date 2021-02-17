using Xunit;
using Compiler.Validate;
using System.Collections.Generic;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSectorsMustHaveValidOwnerTest: AbstractValidatorTestCase
    {
        public AllSectorsMustHaveValidOwnerTest()
        {
            sectorElements.Add(ControllerPositionFactory.Make("BBR"));
            sectorElements.Add(ControllerPositionFactory.Make("BBF"));
            sectorElements.Add(ControllerPositionFactory.Make("LS"));
            sectorElements.Add(ControllerPositionFactory.Make("LC"));
        }

        [Fact]
        public void TestItPassesOnValidControllers()
        {
            sectorElements.Add(
                SectorFactory.Make(
                    owners: SectorOwnerHierarchyFactory.Make(new List<string> {"LS", "LC"})
                )
            );
            sectorElements.Add(
                SectorFactory.Make(
                    owners: SectorOwnerHierarchyFactory.Make(new List<string> {"LS", "LC", "BBR", "BBF"})
                )
            );
            
            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("LS","WHAT")]
        [InlineData("LC", "WHAT")]
        [InlineData("BBR", "WHAT")]
        [InlineData("BBF","WHAT")]
        public void TestItFailsOnInvalidControllers(string first, string second)
        {
            sectorElements.Add(
                SectorFactory.Make(
                    owners: SectorOwnerHierarchyFactory.Make(new List<string> {first})
                )
            );
            sectorElements.Add(
                SectorFactory.Make(
                    owners: SectorOwnerHierarchyFactory.Make(new List<string> {first, second})
                )
            );
            
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSectorsMustHaveValidOwner();
        }
    }
}
