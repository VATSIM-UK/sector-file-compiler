using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllSctStarsMustHaveAValidRoute : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (SidStarRoute star in sectorElements.StarRoutes)
            {
                foreach (RouteSegment segment in star.Segments)
                {
                    if (
                        !RoutePointValidator.ValidatePoint(segment.Start, sectorElements) ||
                        !RoutePointValidator.ValidatePoint(segment.End, sectorElements)
                    ) {
                        string message =
                            $"Invalid segment {segment.GetCompileData(sectorElements)} on SID Route {star.Identifier}";
                        events.AddEvent(
                            new ValidationRuleFailure(message)
                        );
                    }
                }
            }
        }
    }
}
