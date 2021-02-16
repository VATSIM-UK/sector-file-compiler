using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllRunwayExitsMustHaveAValidRunway : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (GroundNetwork groundNetwork in sectorElements.GroundNetworks)
            {
                foreach (GroundNetworkRunwayExit exit in groundNetwork.RunwayExits)
                {
                    if (!RunwayValid(sectorElements, exit.Runway, groundNetwork.Airport))
                    {
                        string message =  $"Invalid ground network runway {groundNetwork.Airport}/{exit.Runway}";
                        events.AddEvent(new ValidationRuleFailure(message, exit));
                        break;
                    }
                }
            }
        }

        private bool RunwayValid(SectorElementCollection sectorElements, string runwayIdentifier, string airportCode)
        {
            List<Airport> airport = sectorElements.Airports.Where(airportElement => airportElement.Icao == airportCode).ToList();

            return airport.Count != 0 && sectorElements.Runways
                .Where(runway => runway.AirfieldIcao == airport[0].Icao)
                .Where(runway => runway.FirstIdentifier == runwayIdentifier || runway.ReverseIdentifier == runwayIdentifier)
                .ToList()
                .Count() != 0;
        }
    }
}
