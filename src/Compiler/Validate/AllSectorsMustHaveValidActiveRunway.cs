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
    public class AllSectorsMustHaveValidActiveRunway : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (Sector sector in sectorElements.Sectors)
            {
                foreach (SectorActive active in sector.Active)
                {
                    if (!this.RunwayValid(sectorElements, active.Runway, active.Airfield))
                    {
                        string message = String.Format(
                            "Invalid ACTIVE runway {0}/{1} on sector {1}",
                            active.Airfield,
                            active.Runway,
                            sector.Name
                        );
                        events.AddEvent(new ValidationRuleFailure(message));
                        break;
                    }
                }
            }
        }

        private bool RunwayValid(SectorElementCollection sectorElements, string runwayIdentifier, string airportCode)
        {
            List<Airport> airport = sectorElements.Airports.Where(airport => airport.Icao == airportCode).ToList();

            return airport.Count != 0 && sectorElements.Runways
                .Where(runway => runway.RunwayDialogDescription == airport[0].Name)
                .Where(runway => runway.FirstIdentifier == runwayIdentifier || runway.ReverseIdentifier == runwayIdentifier)
                .ToList()
                .Count() != 0;
        }
    }
}
