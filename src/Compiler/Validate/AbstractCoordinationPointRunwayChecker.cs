using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Validate
{
    public abstract class AbstractCoordinationPointRunwayChecker
    {
        protected static bool RunwayValid(SectorElementCollection sectorElements, string runwayIdentifier, string airportCode)
        {
            if (airportCode == "*" || airportCode.Length != 4 || runwayIdentifier == "*")
            {
                return true;
            }

            List<Airport> airport = sectorElements.Airports.Where(airportElement => airportElement.Icao == airportCode).ToList();

            if (airport.Count == 0)
            {
                return false;
            }
            
            return sectorElements.Runways
                .Where(runway => runway.AirfieldIcao == airport[0].Icao)
                .Where(runway => runway.FirstIdentifier == runwayIdentifier || runway.ReverseIdentifier == runwayIdentifier)
                .ToList()
                .Count != 0;
        }
    }
}
