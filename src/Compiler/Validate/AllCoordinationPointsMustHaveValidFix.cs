using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllCoordinationPointsMustHaveValidFix : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> airports = sectorElements.Airports.Select(airport => airport.Icao).ToList();
            List<string> fixes = sectorElements.Fixes.Select(fix => fix.Identifier).ToList();
            List<string> vors = sectorElements.Vors.Select(vor => vor.Identifier).ToList();
            List<string> ndbs = sectorElements.Ndbs.Select(ndb => ndb.Identifier).ToList();
            foreach (CoordinationPoint point in sectorElements.CoordinationPoints)
            {
                if (
                    point.CoordinationFix != "*" &&
                    !airports.Contains(point.CoordinationFix) &&
                    !fixes.Contains(point.CoordinationFix) &&
                    !vors.Contains(point.CoordinationFix) &&
                    !ndbs.Contains(point.CoordinationFix)
                ) {
                    string message =
                        $"Invalid fix {point.CoordinationFix} for coordination point: {point.GetCompileData(sectorElements)}";
                    events.AddEvent(new ValidationRuleFailure(message));
                }
            }
        }
    }
}
