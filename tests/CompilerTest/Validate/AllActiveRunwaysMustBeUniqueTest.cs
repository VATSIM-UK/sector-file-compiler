using Compiler.Validate;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Validate
{
    public class AllActiveRunwaysMustBeUniqueTest: AbstractValidatorTestCase
    {
        [Fact]
        public void TestItFailsIfDuplicatesExist()
        {
            sectorElements.Add(ActiveRunwayFactory.Make("EGLL", "27R", 1));
            sectorElements.Add(ActiveRunwayFactory.Make("EGLL", "27R", 1));
            sectorElements.Add(ActiveRunwayFactory.Make("EGLL", "27R", 0));
            sectorElements.Add(ActiveRunwayFactory.Make("EGLL", "27L", 1));
            sectorElements.Add(ActiveRunwayFactory.Make("EGLL", "27L", 1));
            sectorElements.Add(ActiveRunwayFactory.Make("EGLL", "27L", 0));
            sectorElements.Add(ActiveRunwayFactory.Make("EGLL", "27L", 0));
            AssertValidationErrors(3);
        }
        
        [Fact]
        public void TestItPassesOnNoDuplicates()
        {
            sectorElements.Add(ActiveRunwayFactory.Make("EGLL", "27R", 1));
            sectorElements.Add(ActiveRunwayFactory.Make("EGLL", "27R", 0));
            sectorElements.Add(ActiveRunwayFactory.Make("EGLL", "27L", 0));
            sectorElements.Add(ActiveRunwayFactory.Make("EGXY", "27L", 0));
            AssertNoValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllActiveRunwaysMustBeUnique();
        }
    }
}