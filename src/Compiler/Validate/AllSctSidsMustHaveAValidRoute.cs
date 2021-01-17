using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllSctSidsMustHaveAValidRoute : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (SidStarRoute sid in sectorElements.SidRoutes)
            {
                foreach (RouteSegment segment in sid.Segments)
                {
                    if (
                        !RoutePointValidator.ValidatePoint(segment.Start, sectorElements) ||
                        !RoutePointValidator.ValidatePoint(segment.End, sectorElements)
                    ) {
                        string message =
                            $"Invalid segment {segment.GetCompileData(sectorElements)} on SID Route {sid.Identifier}";
                        events.AddEvent(
                            new ValidationRuleFailure(message, segment)
                        );
                    }
                }
            }
        }
    }
}
