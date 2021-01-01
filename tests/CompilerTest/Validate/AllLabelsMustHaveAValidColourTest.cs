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
    public class AllLabelsMustHaveAValidColourTest: AbstractValidatorTestCase
    {
        public AllLabelsMustHaveAValidColourTest()
        {
            this.sectorElements.Add(ColourFactory.Make("colour1"));
        }

        [Fact]
        public void TestItPassesOnValidColours()
        {
            this.sectorElements.Labels.Add(LabelFactory.Make(colour: "colour1"));
            this.sectorElements.Labels.Add(LabelFactory.Make(colour: "123"));

            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidColours()
        {
            this.sectorElements.Labels.Add(LabelFactory.Make(colour: "colour2"));
            this.sectorElements.Labels.Add(LabelFactory.Make(colour: "-123"));

            this.AssertValidationErrors(2);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllLabelsMustHaveAValidColour();
        }
    }
}
