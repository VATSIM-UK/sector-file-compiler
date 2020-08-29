using Compiler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Validate
{
    public abstract class AbstractCoordinationPointRunwayChecker
    {
        protected bool RunwayValid(SectorElementCollection sectorElements, string runwayIdentifier, string airportCode)
        {
            if (airportCode == "*" || airportCode.Length != 4 || runwayIdentifier == "*")
            {
                return true;
            }

            List<Airport> airport = sectorElements.Airports.Where(airport => airport.Icao == airportCode).ToList();

            if (airport.Count == 0)
            {
                return false;
            }

            return sectorElements.Runways
                .Where(runway => runway.RunwayDialogDescription == airport[0].Icao + " " + airport[0].Name)
                .Where(runway => runway.FirstIdentifier == runwayIdentifier || runway.ReverseIdentifier == runwayIdentifier)
                .ToList()
                .Count() != 0;
        }
    }
}
