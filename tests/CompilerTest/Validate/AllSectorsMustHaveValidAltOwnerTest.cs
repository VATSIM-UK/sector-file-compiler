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
                    alternate: new List<SectorAlternateOwnerHierarchy>()
                    {
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string>(){"LS", "LC"}),
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string>(){"BBR"}),
                    }
                )    
            );
            this.sectorElements.Add(
                SectorFactory.Make(
                    alternate: new List<SectorAlternateOwnerHierarchy>()
                    {
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string>(){"LS", "LC"}),
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string>(){"BBR", "LS"}),
                    }
                )    
            );
            
            this.AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("LS","LC", "BBR", "WHAT")]
        [InlineData("BBR","BBF", "LC", "WHAT")]
        [InlineData("LS","LC", "WHAT", "WHAT")]
        public void TestItFailsOnInvalidControllers(string first, string second, string third, string bad)
        {
            this.sectorElements.Add(
                SectorFactory.Make(
                    alternate: new List<SectorAlternateOwnerHierarchy>()
                    {
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string>(){first, second}),
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string>(){second}),
                    }
                )    
            );
            
            this.sectorElements.Add(
                SectorFactory.Make(
                    alternate: new List<SectorAlternateOwnerHierarchy>()
                    {
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string>(){first, second}),
                        SectorAlternateOwnerHierarchyFactory.Make(new List<string>(){second, third, bad, first}),
                    }
                )    
            );
            
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSectorsMustHaveValidAltOwner();
        }
    }
}
