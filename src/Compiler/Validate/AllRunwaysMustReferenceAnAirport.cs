using System;
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
            airports.Add("000A");
            foreach (Runway runway in sectorElements.Runways)
            {
                if (!airports.Contains(runway.AirfieldIcao))
                {
                    string message = String.Format(
                        "Runway {0}/{1} ({2}) does not match up to a defined airport",
                        runway.FirstIdentifier,
                        runway.ReverseIdentifier,
                        runway.AirfieldIcao
                    );

                    events.AddEvent(
                        new ValidationRuleFailure(message)
                    );
                }
            }
        }
    }
}
