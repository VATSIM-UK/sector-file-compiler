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
            this.first = ColourFactory.Make("colour1");
            this.second = ColourFactory.Make("colour2");
            this.third = ColourFactory.Make("colour1");
        }

        [Fact]
        public void TestItPassesIfNoDuplicates()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.second);
            this.AssertNoValidationErrors();;
        }

        [Fact]
        public void TestItFailsIfThereAreDuplicates()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.second);
            this.sectorElements.Add(this.third);
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllColoursMustHaveAUniqueId();
        }
    }
}
