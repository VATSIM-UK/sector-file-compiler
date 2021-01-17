using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllRunwaysMustReferenceAnAirport : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> airports = sectorElements.Airports
                .Select(airport => airport.Icao)
                .ToList();
            
            foreach (Runway runway in sectorElements.Runways)
            {
                if (!airports.Contains(runway.AirfieldIcao))
                {
                    string message =
                        $"Runway {runway.FirstIdentifier}/{runway.ReverseIdentifier} ({runway.AirfieldIcao}) does not match up to a defined airport";

                    events.AddEvent(
                        new ValidationRuleFailure(message, runway)
                    );
                }
            }
        }
    }
}
