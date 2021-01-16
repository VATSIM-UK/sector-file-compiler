using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllCoordinationPointsMustHaveValidArrivalRunways : AbstractCoordinationPointRunwayChecker, IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (CoordinationPoint point in sectorElements.CoordinationPoints)
            {
                if (!RunwayValid(sectorElements, point.ArrivalRunway, point.ArrivalAirportOrFixAfter)) {
                    string message =
                        $"Invalid arrival runway {point.ArrivalRunway}/{point.ArrivalAirportOrFixAfter} for coordination point: {point.GetCompileData(sectorElements)}";
                    events.AddEvent(new ValidationRuleFailure(message));
                }
            }
        }
    }
}
