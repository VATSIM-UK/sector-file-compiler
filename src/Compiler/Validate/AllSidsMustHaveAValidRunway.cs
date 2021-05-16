using Compiler.Event;
using Compiler.Error;
using Compiler.Model;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllSidsMustHaveAValidRunway : AbstractCoordinationPointRunwayChecker, IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach(SidStar sidStar in sectorElements.SidStars)
            {
                if (!RunwayValid(sectorElements, sidStar.Runway, sidStar.Airport)) {
                    string errorMessage =
                        $"Runway {sidStar.Airport}/{sidStar.Runway} is not valid for {sidStar.Type}/{sidStar.Identifier}";
                    events.AddEvent(new ValidationRuleFailure(errorMessage, sidStar));
                }
            }
        }
    }
}
