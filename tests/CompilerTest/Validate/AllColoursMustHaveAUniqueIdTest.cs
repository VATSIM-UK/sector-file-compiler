using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllColoursMustHaveAUniqueIdTest: AbstractValidatorTestCase
    {
        private readonly Colour first;
        private readonly Colour second;
        private readonly Colour third;

        public AllColoursMustHaveAUniqueIdTest()
        {
            first = ColourFactory.Make("colour1");
            second = ColourFactory.Make("colour2");
            third = ColourFactory.Make("colour1");
        }

        [Fact]
        public void TestItPassesIfNoDuplicates()
        {
            sectorElements.Add(first);
            sectorElements.Add(second);
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsIfThereAreDuplicates()
        {
            sectorElements.Add(first);
            sectorElements.Add(second);
            sectorElements.Add(third);
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllColoursMustHaveAUniqueId();
        }
    }
}
