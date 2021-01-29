using Compiler.Validate;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Validate
{
    public class AllActiveRunwaysMustReferenceARunwayTest: AbstractValidatorTestCase
    {
        public AllActiveRunwaysMustReferenceARunwayTest()
        {
            sectorElements.Add(RunwayFactory.Make("EGLL", "09R", "27L"));
            sectorElements.Add(RunwayFactory.Make("EGKK", "26L", "08R"));
        }
        
        [Theory]
        [InlineData("EGLL", "27R")]
        [InlineData("EGKK", "26R")]
        [InlineData("EGLL", "abc")]
        [InlineData("EGGD", "09")]
        public void TestItFailsIfRunwayDoesntExist(string icao, string identifier)
        {
            sectorElements.Add(ActiveRunwayFactory.Make(icao, identifier));
            AssertValidationErrors();
        }
        
        [Theory]
        [InlineData("EGLL", "09R")] // First identifier match
        [InlineData("EGKK", "26L")] // First identifier match
        [InlineData("EGLL", "27L")] // Second identifier match
        [InlineData("EGKK", "08R")] // Second identifier match
        public void TestItPassesIfRunwayExists(string icao, string identifier)
        {
            sectorElements.Add(ActiveRunwayFactory.Make(icao, identifier));
            AssertNoValidationErrors();
        }
        
        protected override IValidationRule GetValidationRule()
        {
            return new AllActiveRunwaysMustReferenceARunway();
        }
    }
}