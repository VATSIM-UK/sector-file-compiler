using Xunit;
using Compiler.Model;
using Compiler.Validate;
using System.Collections.Generic;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSectorsMustHaveValidAltOwnerTest: AbstractValidatorTestCase
    {
        public AllSectorsMustHaveValidAltOwnerTest()
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
                    alternate: new List<SectorAlternateOwnerHierarchy>
                    {
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string> {"LS", "LC"}),
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string> {"BBR"}),
                    }
                )    
            );
            sectorElements.Add(
                SectorFactory.Make(
                    alternate: new List<SectorAlternateOwnerHierarchy>
                    {
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string> {"LS", "LC"}),
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string> {"BBR", "LS"}),
                    }
                )    
            );
            
            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("LS","LC", "BBR", "WHAT")]
        [InlineData("BBR","BBF", "LC", "WHAT")]
        [InlineData("LS","LC", "WHAT", "WHAT")]
        public void TestItFailsOnInvalidControllers(string first, string second, string third, string bad)
        {
            sectorElements.Add(
                SectorFactory.Make(
                    alternate: new List<SectorAlternateOwnerHierarchy>
                    {
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string> {first, second}),
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string> {second}),
                    }
                )    
            );
            
            sectorElements.Add(
                SectorFactory.Make(
                    alternate: new List<SectorAlternateOwnerHierarchy>
                    {
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string> {first, second}),
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string> {second, third, bad, first}),
                    }
                )    
            );
            
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSectorsMustHaveValidAltOwner();
        }
    }
}
