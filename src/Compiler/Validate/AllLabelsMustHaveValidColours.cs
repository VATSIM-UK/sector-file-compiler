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
                    string errorMessage = $"Invalid colour value {label.Colour} in label {label.Text}";
                    events.AddEvent(new ValidationRuleFailure(errorMessage));
                }
            }
        }
    }
}
