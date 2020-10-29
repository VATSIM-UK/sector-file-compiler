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
    public class AllSectorsMustHaveValidDepartureAirports : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> airports = sectorElements.Airports.Select(airport => airport.Icao).ToList();
            foreach (Sector sector in sectorElements.Sectors)
            {
                foreach (SectorDepartureAirports departureAirports in sector.DepartureAirports)
                {
                    foreach (string airport in departureAirports.Airports)
                    {
                        if (!airports.Contains(airport))
                        {
                            string message = String.Format(
                                "Invalid DEPAPT {0} on sector {1}",
                                airport,
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
}
