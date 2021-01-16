using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllSectorsMustHaveValidArrivalAirports : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> airports = sectorElements.Airports.Select(airport => airport.Icao).ToList();
            foreach (Sector sector in sectorElements.Sectors)
            {
                foreach (SectorArrivalAirports arrivalAirport in sector.ArrivalAirports)
                {
                    foreach (string airport in arrivalAirport.Airports)
                    {
                        if (!airports.Contains(airport))
                        {
                            string message = $"Invalid ARRAPT {airport} on sector {sector.Name}";
                            events.AddEvent(new ValidationRuleFailure(message));
                            break;
                        }
                    }
                }
            }
        }
    }
}
