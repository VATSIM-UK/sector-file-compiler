﻿using System.Collections.Generic;
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
            TestAirwayCategory(sectorElements.LowAirways, sectorElements, events);
            TestAirwayCategory(sectorElements.HighAirways, sectorElements, events);
        }

        private void TestAirwayCategory(
            List<AirwaySegment> airways,
            SectorElementCollection sectorElements,
            IEventLogger events
        ) {
            foreach (AirwaySegment airway in airways)
            {
                if (airway.StartPoint.Type() == Point.TypeIdentifier)
                {
                    if (InvalidPoint(airway.StartPoint.Identifier, sectorElements))
                    {
                        string message =
                            $"Invalid end point {airway.StartPoint.Identifier} on Airway segment for {airway.Identifier}";
                        events.AddEvent(
                            new ValidationRuleFailure(message, airway)
                        );
                    }
                }

                if (airway.EndPoint.Type() == Point.TypeIdentifier)
                {
                    if (InvalidPoint(airway.EndPoint.Identifier, sectorElements))
                    {
                        string message =
                            $"Invalid start point {airway.EndPoint.Identifier} on Airway segment for {airway.Identifier}";
                        events.AddEvent(
                            new ValidationRuleFailure(message, airway)
                        );
                    }
                }
            }
        }

        private bool InvalidPoint(string identifier, SectorElementCollection sectorElements)
        {
            return !FindFixByIdentifier(identifier, sectorElements) &&
            !FindVorByIdentifier(identifier, sectorElements) &&
            !FindNdbByIdentifier(identifier, sectorElements) &&
            !FindAirportByIdentifier(identifier, sectorElements);
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
