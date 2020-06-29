using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllSectorsMustHaveValidGuestAirports : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> airports = sectorElements.Airports.Select(airport => airport.Icao).ToList();
            foreach (Sector sector in sectorElements.Sectors)
            {
                foreach (SectorGuest guest in sector.Guests)
                {
                    if (guest.DepartureAirport != "*" && !airports.Contains(guest.DepartureAirport))
                    {
                        string message = String.Format(
                            "Invalid departure GUEST airport {0} on sector {1}",
                            guest.DepartureAirport,
                            sector.Name
                        );
                        events.AddEvent(new ValidationRuleFailure(message));
                        break;
                    }

                    if (guest.ArrivalAirport != "*" && !airports.Contains(guest.ArrivalAirport))
                    {
                        string message = String.Format(
                            "Invalid arrival GUEST airport {0} on sector {1}",
                            guest.ArrivalAirport,
                            sector.Name
                        );
                        events.AddEvent(new ValidationRuleFailure(message));
                        break;
                    }
                }
            }
        }
    }
}
