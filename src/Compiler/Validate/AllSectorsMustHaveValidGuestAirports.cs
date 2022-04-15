using System.Collections.Generic;
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
                    if (guest.DepartureAirport != "*" && IsHomeAirport(guest.DepartureAirport) &&
                        !airports.Contains(guest.DepartureAirport))
                    {
                        string message =
                            $"Invalid departure GUEST airport {guest.DepartureAirport} on sector {sector.Name}";
                        events.AddEvent(new ValidationRuleFailure(message, guest));
                        break;
                    }

                    if (guest.ArrivalAirport != "*" && IsHomeAirport(guest.ArrivalAirport) &&
                        !airports.Contains(guest.ArrivalAirport))
                    {
                        string message =
                            $"Invalid arrival GUEST airport {guest.ArrivalAirport} on sector {sector.Name}";
                        events.AddEvent(new ValidationRuleFailure(message, guest));
                        break;
                    }
                }
            }
        }

        private bool IsHomeAirport(string airport)
        {
            return airport.StartsWith("EG");
        }
    }
}
