using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllSidsMustHaveAValidRoute : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (SidStar sidStar in sectorElements.SidStars)
            {
                foreach (string waypoint in sidStar.Route)
                {
                    if (
                        !RoutePointValidator.ValidateEseSidStarPoint(waypoint, sectorElements) &&
                        !RoutePointValidator.ValidateEseSidStarPoint(waypoint, sectorElements) &&
                        !RoutePointValidator.ValidateEseSidStarPoint(waypoint, sectorElements) &&
                        !RoutePointValidator.ValidateEseSidStarPoint(waypoint, sectorElements)
                    ) {
                        string message = String.Format(
                            "Invalid waypoint {0} on SID/STAR {1}/{2}",
                            waypoint,
                            sidStar.Airport,
                            sidStar.Identifier
                        );
                        events.AddEvent(
                            new ValidationRuleFailure(message)
                        );
                    }
                }
            }
        }
    }
}
