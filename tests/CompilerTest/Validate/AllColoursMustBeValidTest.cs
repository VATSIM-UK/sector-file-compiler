using Xunit;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Validate;
using Moq;
using Compiler.Argument;
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
            this.first = ColourFactory.Make("colour1", -1);
            this.second = ColourFactory.Make("colour2", 0);
            this.third = ColourFactory.Make("colour3", 255);
            this.fourth = ColourFactory.Make("colour4", 16777215);
            this.fifth = ColourFactory.Make("colour5", 16777216);
        }

        [Fact]
        public void TestItPassesOnValidColours()
        {
            this.sectorElements.Add(this.second);
            this.sectorElements.Add(this.third);
            this.sectorElements.Add(this.fourth);
            this.AssertNoValidationError();;
        }

        [Fact]
        public void TestItFailsOnNegativeValues()
        {
            this.sectorElements.Add(this.first);
            this.AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnValuesOverMaximum()
        {
            this.sectorElements.Add(this.fifth);
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllColoursMustBeValid();
        }
    }
}
