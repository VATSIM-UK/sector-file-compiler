using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllAirwaysMustHaveValidPoints : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            this.TestAirwayCategory(sectorElements.LowAirways, sectorElements, events);
            this.TestAirwayCategory(sectorElements.HighAirways, sectorElements, events);
        }

        private void TestAirwayCategory(
            List<Airway> airways,
            SectorElementCollection sectorElements,
            IEventLogger events
        ) {
            foreach (Airway airway in airways)
            {
                if (airway.StartPoint.Type() == Point.TYPE_IDENTIFIER)
                {
                    if (this.InvalidPoint(airway.StartPoint.Identifier, sectorElements))
                    {
                        string message = String.Format(
                            "Invalid end point {0} on Airway segment for {1}",
                            airway.StartPoint.Identifier,
                            airway.Identifier
                        );
                        events.AddEvent(
                            new ValidationRuleFailure(message)
                        );
                    }
                }

                if (airway.EndPoint.Type() == Point.TYPE_IDENTIFIER)
                {
                    if (this.InvalidPoint(airway.EndPoint.Identifier, sectorElements))
                    {
                        string message = String.Format(
                            "Invalid start point {0} on Airway segment for {1}",
                            airway.EndPoint.Identifier,
                            airway.Identifier
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
