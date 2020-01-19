using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllLabelsMustHaveAValidColour : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (Label label in sectorElements.Labels)
            {
                if (!ColourValidator.ColourValid(sectorElements, label.Colour))
                {
                    string errorMessage = string.Format(
                        "Invalid colour value {0} in label {1}",
                        label.Colour,
                        label.Text
                    );
                    events.AddEvent(new ValidationRuleFailure(errorMessage));
                }
            }
        }
    }
}
