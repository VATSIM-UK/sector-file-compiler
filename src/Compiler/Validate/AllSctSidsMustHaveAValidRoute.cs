using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;

namespace Compiler.Validate
{
    public class AllSctSidsMustHaveAValidRoute : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, IEventLogger events)
        {
            foreach (SidStarRoute sid in sectorElements.SidRoutes)
            {
                foreach (RouteSegment segment in sid.Segments)
                {
                    if (
                        CheckPoint(segment.Start, sectorElements) ||
                        CheckPoint(segment.End, sectorElements)
                    ) {
                        string message = String.Format(
                            "Invalid segment {0} on SID Route {1}",
                            segment.Compile(),
                            sid.Identifier
                        );
                        events.AddEvent(
                            new ValidationRuleFailure(message)
                        );
                    }
                }
            }
        }

        private bool CheckPoint(Point point, SectorElementCollection sectorElements)
        {
            return !FindCoordinate(point) &&
                !FindFixByIdentifier(point.Identifier, sectorElements) &&
                !FindVorByIdentifier(point.Identifier, sectorElements) &&
                !FindNdbByIdentifier(point.Identifier, sectorElements) &&
                !FindAirportByIdentifier(point.Identifier, sectorElements);
        }

        private bool FindCoordinate(Point point)
        {
            return point.Type() == Point.TYPE_COORDINATE;
        }

        private bool FindVorByIdentifier(string identifier, SectorElementCollection sectorElements)
        {
            foreach (Vor vor in sectorElements.Vors)
            {
                if (vor.Identifier == identifier)
                {
                    return true;
                }
            }

            return false;
        }

        private bool FindNdbByIdentifier(string identifier, SectorElementCollection sectorElements)
        {
            foreach (Ndb ndb in sectorElements.Ndbs)
            {
                if (ndb.Identifier == identifier)
                {
                    return true;
                }
            }

            return false;
        }

        private bool FindAirportByIdentifier(string identifier, SectorElementCollection sectorElements)
        {
            foreach (Airport airport in sectorElements.Airports)
            {
                if (airport.Icao == identifier)
                {
                    return true;
                }
            }

            return false;
        }

        private bool FindFixByIdentifier(string identifier, SectorElementCollection sectorElements)
        {
            foreach (Fix fix in sectorElements.Fixes)
            {
                if (fix.Identifier == identifier)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
