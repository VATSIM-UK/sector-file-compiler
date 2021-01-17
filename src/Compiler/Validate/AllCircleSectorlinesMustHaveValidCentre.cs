using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllCircleSectorlinesMustHaveValidCentre : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> airports = sectorElements.Airports.Select(airport => airport.Icao).ToList();
            List<string> fixes = sectorElements.Fixes.Select(fix => fix.Identifier).ToList();
            List<string> vors = sectorElements.Vors.Select(vor => vor.Identifier).ToList();
            List<string> ndbs = sectorElements.Ndbs.Select(ndb => ndb.Identifier).ToList();
            foreach (CircleSectorline circle in sectorElements.CircleSectorLines)
            {
                if (
                    circle.CentrePoint != null &&
                    !airports.Contains(circle.CentrePoint) &&
                    !fixes.Contains(circle.CentrePoint) &&
                    !vors.Contains(circle.CentrePoint) &&
                    !ndbs.Contains(circle.CentrePoint)
                ) {
                    string message =
                        $"Invalid fix {circle.CentrePoint} for CIRCLE_SECTORLINE: {circle.GetCompileData(sectorElements)}";
                    events.AddEvent(new ValidationRuleFailure(message, circle));
                }
            }
        }
    }
}
