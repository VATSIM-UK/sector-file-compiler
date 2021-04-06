using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AltOwnersMayOnlyAppearOnceInEachAltOwnershipLineTest: AbstractValidatorTestCase
    {
        private readonly Sector sector1;
        private readonly Sector sector2;
        private readonly Sector sector3;
        private readonly Sector sector4;

        public AltOwnersMayOnlyAppearOnceInEachAltOwnershipLineTest()
        {
            sector1 = SectorFactory.Make(
                alternate: new List<SectorAlternateOwnerHierarchy>
                {
                    SectorAlternateOwnerHierarchyFactory.Make(new List<string> {"KKD", "KKG", "KKT"}),
                    SectorAlternateOwnerHierarchyFactory.Make(new List<string> {"KKD", "KKG", "KKT"}),
                }
            );
            sector2 = SectorFactory.Make(
                alternate: new List<SectorAlternateOwnerHierarchy>
                {
                    SectorAlternateOwnerHierarchyFactory.Make(new List<string> {"LLD", "LLG", "LLG2"}),
                    SectorAlternateOwnerHierarchyFactory.Make(new List<string> {"LLN", "LLS", "LS"}),
                }
            );
            sector3 = SectorFactory.Make(
                alternate: new List<SectorAlternateOwnerHierarchy>
                {
                    SectorAlternateOwnerHierarchyFactory.Make(new List<string> {"LLD", "LLG", "LLG2", "LLT", "LLD", "LLT"}),
                    SectorAlternateOwnerHierarchyFactory.Make(new List<string> {"LLD", "LLG", "LLG2"}),
                    SectorAlternateOwnerHierarchyFactory.Make(new List<string> {"LLN", "LLS", "LS", "LLN"})
                }
            );
            sector4 = SectorFactory.Make(
                alternate: new List<SectorAlternateOwnerHierarchy>
                {
                    SectorAlternateOwnerHierarchyFactory.Make(new List<string> {"LLD", "LLG", "LLG2", "LLG"}),
                    SectorAlternateOwnerHierarchyFactory.Make(new List<string> {"LLN", "LLS", "LS"})
                }
            );
        }

        [Fact]
        public void TestItPassesOnNoDuplicates()
        {
            sectorElements.Add(sector1);
            sectorElements.Add(sector2);

            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnDuplicates()
        {
            sectorElements.Add(sector1);
            sectorElements.Add(sector2);
            sectorElements.Add(sector3);
            sectorElements.Add(sector4);

            AssertValidationErrors(4);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AltOwnersMayOnlyAppearOnceInEachAltOwnershipLine();
        }
    }
}
