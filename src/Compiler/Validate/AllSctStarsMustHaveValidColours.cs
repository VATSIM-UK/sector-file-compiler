using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllSctStarsMustHaveValidColours : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (SidStarRoute star in sectorElements.StarRoutes)
            {
                foreach (RouteSegment segment in star.Segments)
                {
                    if (segment.Colour != null && !ColourValidator.ColourValid(sectorElements, segment.Colour))
                    {
                        string errorMessage =
                            $"Invalid colour value {segment.Colour} in route segment for STAR {star.Identifier}";
                        events.AddEvent(new ValidationRuleFailure(errorMessage));
                    }
                }
            }
        }
    }
}
