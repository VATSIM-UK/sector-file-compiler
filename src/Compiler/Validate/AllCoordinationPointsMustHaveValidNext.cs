using System;
using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllCoordinationPointsMustHaveValidNext : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> fixes = sectorElements.Fixes.Select(fix => fix.Identifier).ToList();
            List<string> vors = sectorElements.Vors.Select(vor => vor.Identifier).ToList();
            List<string> ndbs = sectorElements.Ndbs.Select(ndb => ndb.Identifier).ToList();
            foreach (CoordinationPoint point in sectorElements.CoordinationPoints)
            {
                if (
                    point.ArrivalAirportOrFixAfter != "*" &&
                    !AirportValidator.IcaoValid(point.ArrivalAirportOrFixAfter) &&
                    !fixes.Contains(point.ArrivalAirportOrFixAfter) &&
                    !vors.Contains(point.ArrivalAirportOrFixAfter) &&
                    !ndbs.Contains(point.ArrivalAirportOrFixAfter)
                ) {
                    string message = String.Format(
                        "Invalid next fix or arrival airport {0} on coordination point: {1}",
                        point.ArrivalAirportOrFixAfter,
                        point.GetCompileData(sectorElements)
                    );
                    events.AddEvent(new ValidationRuleFailure(message));
                    continue;
                }
            }
        }
    }
}
