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
                    if (segment.Colour != null && !ColourValidator.ColourValid(sectorElements, segment.Colour))
                    {
                        string errorMessage =
                            $"Invalid colour value {segment.Colour} in route segment for SID {sid.Identifier}";
                        events.AddEvent(new ValidationRuleFailure(errorMessage, segment));
                    }
                }
            }
        }
    }
}
