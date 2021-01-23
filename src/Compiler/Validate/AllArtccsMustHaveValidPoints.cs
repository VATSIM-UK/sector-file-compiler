using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllArtccsMustHaveValidPoints : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            this.TestArtccCategory(sectorElements.Artccs, sectorElements, events);
            this.TestArtccCategory(sectorElements.LowArtccs, sectorElements, events);
            this.TestArtccCategory(sectorElements.HighArtccs, sectorElements, events);
        }

        private void TestArtccCategory(
            List<ArtccSegment> artccs,
            SectorElementCollection sectorElements,
            IEventLogger events
        ) {
            foreach (ArtccSegment artcc in artccs)
            {
                if (artcc.StartPoint.Type() == Point.TypeIdentifier)
                {
                    if (this.InvalidPoint(artcc.StartPoint.Identifier, sectorElements))
                    {
                        string message =
                            $"Invalid end point {artcc.StartPoint.Identifier} on ARTCC segment for {artcc.Identifier}";
                        events.AddEvent(
                            new ValidationRuleFailure(message, artcc)
                        );
                    }
                }

                if (artcc.EndPoint.Type() == Point.TypeIdentifier)
                {
                    if (this.InvalidPoint(artcc.EndPoint.Identifier, sectorElements))
                    {
                        string message =
                            $"Invalid start point {artcc.EndPoint.Identifier} on ARTCC segment for {artcc.Identifier}";
                        events.AddEvent(
                            new ValidationRuleFailure(message, artcc)
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
