using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllAirportsMustHaveUniqueCode : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> existingAirports = new List<string>();
            foreach (Airport airport in sectorElements.Airports)
            {
                if (existingAirports.Contains(airport.Icao))
                {
                    events.AddEvent(new ValidationRuleFailure("Duplicate airport " + airport.Icao, airport));
                    continue;
                }

                existingAirports.Add(airport.Icao);
            }
        }
    }
}
