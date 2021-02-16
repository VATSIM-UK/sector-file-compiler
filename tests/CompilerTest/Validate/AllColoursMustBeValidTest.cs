using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllColoursMustBeValidTest: AbstractValidatorTestCase
    {
        private readonly Colour first;
        private readonly Colour second;
        private readonly Colour third;
        private readonly Colour fourth;
        private readonly Colour fifth;

        public AllColoursMustBeValidTest()
        {
            first = ColourFactory.Make("colour1", -1);
            second = ColourFactory.Make("colour2", 0);
            third = ColourFactory.Make("colour3", 255);
            fourth = ColourFactory.Make("colour4", 16777215);
            fifth = ColourFactory.Make("colour5", 16777216);
        }

        [Fact]
        public void TestItPassesOnValidColours()
        {
            sectorElements.Add(second);
            sectorElements.Add(third);
            sectorElements.Add(fourth);
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnNegativeValues()
        {
            sectorElements.Add(first);
            AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnValuesOverMaximum()
        {
            sectorElements.Add(fifth);
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllColoursMustBeValid();
        }
    }
}
