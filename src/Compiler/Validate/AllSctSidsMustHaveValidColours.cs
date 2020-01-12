using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllSctSidsMustHaveValidColours : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (SidStarRoute sid in sectorElements.SidRoutes)
            {
                foreach (RouteSegment segment in sid.Segments)
                {
                    if (segment.Colour != null && !this.ColourValid(sectorElements, segment.Colour))
                    {
                        string errorMessage = string.Format(
                            "Invalid colour value {0} in route segment for SID {1}",
                            segment.Colour,
                            sid.Identifier
                        );
                        events.AddEvent(new ValidationRuleFailure(errorMessage));
                        break;
                    }
                }
            }
        }

        private bool ColourValid(SectorElementCollection sectorElements, string colour)
        {
            return (int.TryParse(colour, out int colourValue) && colourValue >= 0 && colourValue <= 16777215) ||
                this.ColourIsDefined(sectorElements, colour);
        }

        private bool ColourIsDefined(SectorElementCollection sectorElements, string colourString)
        {
            foreach (Colour colour in sectorElements.Colours)
            {
                if (colourString == colour.Name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
