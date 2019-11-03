using Compiler.Event;
using Compiler.Model;
using Compiler.Error;

namespace Compiler.Validate
{
    public class AllColoursMustBeValid : IValidationRule
    {
        const int RED_BITMASK = 255;
        const int GREEN_BITMASK = 65280;
        const int BLUE_BITMASK = 16711680;

        public void Validate(SectorElementCollection sectorElements, IEventLogger events)
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
