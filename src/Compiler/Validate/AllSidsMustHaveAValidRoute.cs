using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;

namespace Compiler.Validate
{
    public class AllSidsMustHaveAValidRoute : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, IEventLogger events)
        {
            foreach (SidStar sidStar in sectorElements.SidStars)
            {
                foreach (string waypoint in sidStar.Route)
                {
                    if (
                        !FindFixByIdentifier(waypoint, sectorElements) &&
                        !FindVorByIdentifier(waypoint, sectorElements) &&
                        !FindNdbByIdentifier(waypoint, sectorElements) &&
                        !FindAirportByIdentifier(waypoint, sectorElements)
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
