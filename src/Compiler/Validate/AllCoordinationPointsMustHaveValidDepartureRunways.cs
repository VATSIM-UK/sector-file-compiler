using System;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllCoordinationPointsMustHaveValidDepartureRunways : AbstractCoordinationPointRunwayChecker, IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (CoordinationPoint point in sectorElements.CoordinationPoints)
            {
                if (!RunwayValid(sectorElements, point.DepartureRunway, point.DepartureAirportOrFixBefore)) {
                    string message =
                        $"Invalid departure runway {point.DepartureRunway}/{point.DepartureAirportOrFixBefore} for coordination point: {point.GetCompileData(sectorElements)}";
                    events.AddEvent(new ValidationRuleFailure(message));
                    continue;
                }
            }
        }
    }
}
