using System.Collections.Generic;
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
                        this.RunwayValid(sectorElements, active.Runway, active.Airfield);
                        string message =
                            $"Invalid ACTIVE runway {active.Airfield}/{active.Runway} on sector {sector.Name}";
                        events.AddEvent(new ValidationRuleFailure(message, active));
                        break;
                    }
                }
            }
        }

        private bool RunwayValid(SectorElementCollection sectorElements, string runwayIdentifier, string airportCode)
        {
            if (airportCode == "000A" && (runwayIdentifier == "00" || runwayIdentifier == "01"))
            {
                return true;
            }

            List<Airport> airport = sectorElements.Airports.Where(airportElement => airportElement.Icao == airportCode).ToList();

            return airport.Count != 0 && sectorElements.Runways
                .Where(runway => runway.AirfieldIcao == airport[0].Icao)
                .Where(runway => runway.FirstIdentifier == runwayIdentifier || runway.ReverseIdentifier == runwayIdentifier)
                .ToList()
                .Count() != 0;
        }
    }
}
