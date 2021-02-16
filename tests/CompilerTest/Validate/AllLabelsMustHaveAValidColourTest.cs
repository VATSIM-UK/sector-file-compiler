using Xunit;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllLabelsMustHaveAValidColourTest: AbstractValidatorTestCase
    {
        public AllLabelsMustHaveAValidColourTest()
        {
            sectorElements.Add(ColourFactory.Make("colour1"));
        }

        [Fact]
        public void TestItPassesOnValidColours()
        {
            sectorElements.Labels.Add(LabelFactory.Make(colour: "colour1"));
            sectorElements.Labels.Add(LabelFactory.Make(colour: "123"));

            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidColours()
        {
            sectorElements.Labels.Add(LabelFactory.Make(colour: "colour2"));
            sectorElements.Labels.Add(LabelFactory.Make(colour: "-123"));

            AssertValidationErrors(2);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllLabelsMustHaveAValidColour();
        }
    }
}
