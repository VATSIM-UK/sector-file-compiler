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
                if (!this.ColourValid(colour))
                {
                    events.AddEvent(new ValidationRuleFailure("Invalid colour value " + colour.Value));
                    continue;
                }
            }
        }

        private bool ColourValid(Colour colour)
        {
            return colour.Value >= 0 &&
                colour.Value <= 16777215;
        }
    }
}
