using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;

namespace Compiler.Validate
{
    public class AllArtccsMustHaveValidPoints : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, IEventLogger events)
        {
            this.TestArtccCategory(sectorElements.Artccs, sectorElements, events);
            this.TestArtccCategory(sectorElements.LowArtccs, sectorElements, events);
            this.TestArtccCategory(sectorElements.HighArtccs, sectorElements, events);
        }

        private void TestArtccCategory(
            List<Artcc> artccs,
            SectorElementCollection sectorElements,
            IEventLogger events
        ) {
            foreach (Artcc artcc in artccs)
            {
                if (artcc.StartPoint.Type() == Point.TYPE_IDENTIFIER)
                {
                    if (this.InvalidPoint(artcc.StartPoint.Identifier, sectorElements))
                    {
                        string message = String.Format(
                            "Invalid end point {0} on ARTCC segment for {1}",
                            artcc.StartPoint.Identifier,
                            artcc.Identifier
                        );
                        events.AddEvent(
                            new ValidationRuleFailure(message)
                        );
                    }
                }

                if (artcc.EndPoint.Type() == Point.TYPE_IDENTIFIER)
                {
                    if (this.InvalidPoint(artcc.EndPoint.Identifier, sectorElements))
                    {
                        string message = String.Format(
                            "Invalid start point {0} on ARTCC segment for {1}",
                            artcc.EndPoint.Identifier,
                            artcc.Identifier
                        );
                        events.AddEvent(
                            new ValidationRuleFailure(message)
                        );
                    }
                }
            }
        }

        private bool InvalidPoint(string identifier, SectorElementCollection sectorElements)
        {
            return !this.FindFixByIdentifier(identifier, sectorElements) &&
            !this.FindVorByIdentifier(identifier, sectorElements) &&
            !this.FindNdbByIdentifier(identifier, sectorElements) &&
            !this.FindAirportByIdentifier(identifier, sectorElements);
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
