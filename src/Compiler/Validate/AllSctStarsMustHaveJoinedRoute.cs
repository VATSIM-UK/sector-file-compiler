using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;

namespace Compiler.Validate
{
    public class AllSctStarsMustHaveJoinedRoute : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, IEventLogger events)
        {
            foreach (SidStarRoute sid in sectorElements.StarRoutes)
            {
                if (!CheckRoute(sid.Segments))
                {
                    string message = String.Format(
                        "STAR route is not continuous for {0}",
                        sid.Identifier
                    );
                    events.AddEvent(
                        new ValidationRuleFailure(message)
                    );
                }
            }
        }

        private bool CheckRoute(List<RouteSegment> segments)
        {
            if (segments.Count == 1)
            {
                return true;
            }

            for (int i = 1; i < segments.Count; i++)
            {
                if (!segments[i - 1].End.Equals(segments[i].Start)) {
                    return false;
                }
            }

            return true;
        }
    }
}
