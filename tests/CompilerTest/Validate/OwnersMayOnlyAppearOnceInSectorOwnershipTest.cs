using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class OwnersMayOnlyAppearOnceInSectorOwnershipTest: AbstractValidatorTestCase
    {
        private readonly Sector sector1;
        private readonly Sector sector2;
        private readonly Sector sector3;
        private readonly Sector sector4;

        public OwnersMayOnlyAppearOnceInSectorOwnershipTest()
        {
            sector1 = SectorFactory.Make(
                owners: SectorOwnerHierarchyFactory.Make(new List<string>() {"KKD", "KKG", "KKT"})
            );
            sector2 = SectorFactory.Make(
                owners: SectorOwnerHierarchyFactory.Make(new List<string>() {"LLD", "LLG", "LLG2"})
            );
            sector3 = SectorFactory.Make(
                owners: SectorOwnerHierarchyFactory.Make(new List<string>() {"LLD", "LLG", "LLG2", "LLT", "LLD", "LLT"})
            );
            sector4 = SectorFactory.Make(
                owners: SectorOwnerHierarchyFactory.Make(new List<string>() {"LLD", "LLG", "LLG2", "LLG"})
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

            AssertValidationErrors(3);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new OwnersMayOnlyAppearOnceInSectorOwnership();
        }
    }
}
