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
    public class AllRunwayDescriptionsMustReferenceAnAirport : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> airportDescriptions = sectorElements.Airports.Select(airport => airport.Name).ToList();
            foreach (Runway runway in sectorElements.Runways)
            {
                if (!airportDescriptions.Contains(runway.RunwayDialogDescription))
                {
                    string message = String.Format(
                        "Runway {0}/{1} ({2}) does not match up to a defined airport name",
                        runway.FirstIdentifier,
                        runway.ReverseIdentifier,
                        runway.RunwayDialogDescription
                    );

                    events.AddEvent(
                        new ValidationRuleFailure(message)
                    );
                }
            }
        }
    }
}
