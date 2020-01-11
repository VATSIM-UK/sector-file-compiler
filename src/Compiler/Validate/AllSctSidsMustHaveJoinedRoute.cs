using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;

namespace Compiler.Validate
{
    public class AllSctSidsMustHaveJoinedRoute : IValidationRule
    {
        // A coordinate that some routes have at the start, so we ignore it for validation
        private readonly RouteSegment defaultStarter = new RouteSegment(
            new Point(new Coordinate("S999.00.00.000", "E999.00.00.000")),
            new Point(new Coordinate("S999.00.00.000", "E999.00.00.000")),
            null
        );

        public void Validate(SectorElementCollection sectorElements, IEventLogger events)
        {
            foreach (SidStarRoute sid in sectorElements.SidRoutes)
            {
                if (sid.Identifier == "EGAA Aldergrove SMAA")
                {
                    bool test = true;
                }
                if (!CheckRoute(sid.Segments))
                {
                    string message = String.Format(
                        "SID route is not continuous for {0}",
                        sid.Identifier
                    );
                    events.AddEvent(
                        new ValidationRuleFailure(message)
                    );
                }
            }
        }

        // Make sure the route is all lovely and joined
        private bool CheckRoute(List<RouteSegment> segments)
        {
            if (
                segments.Count == 1 ||
                (segments.Count == 2 && segments[0] == this.defaultStarter)
            )
            {
                return true;
            }

            for (int i = 2; i < segments.Count; i++)
            {
                if (!segments[i - 1].End.Equals(segments[i].Start))
                {
                    string seg1 = segments[i - 1].Compile();
                    string seg2 = segments[i].Compile();
                    return false;
                }
            }

            return true;
        }
    }
}
