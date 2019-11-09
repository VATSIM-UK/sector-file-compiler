using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;

namespace Compiler.Validate
{
    public class AllAirportsMustHaveUniqueCode : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, IEventLogger events)
        {
            List<string> existingAirports = new List<string>();
            foreach (Airport airport in sectorElements.Airports)
            {
                if (existingAirports.Contains(airport.Icao))
                {
                    events.AddEvent(new ValidationRuleFailure("Duplicate airport " + airport.Icao));
                    continue;
                }

                existingAirports.Add(airport.Icao);
            }
        }
    }
}
