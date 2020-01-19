using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllColoursMustBeValid : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (Colour colour in sectorElements.Colours)
            {
                if (!ColourValidator.IsValidColourInteger(colour.Value.ToString()))
                {
                    events.AddEvent(new ValidationRuleFailure("Invalid colour value " + colour.Value));
                }
            }
        }
    }
}
