using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Event;
using Compiler.Error;
using Compiler.Model;

namespace Compiler.Validate
{
    public class AllSidsMustHaveAValidAirport : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, IEventLogger events)
        {
            foreach(SidStar sidStar in sectorElements.SidStars)
            {
                bool airportFound = false;
                foreach (Airport airport in sectorElements.Airports)
                {
                    if (sidStar.Airport == airport.Icao)
                    {
                        airportFound = true;
                        break;
                    }
                }

                if (!airportFound)
                {
                    string errorMessage = String.Format(
                        "Airport {0} is not valid for {1}/{2}",
                        sidStar.Airport,
                        sidStar.Type,
                        sidStar.Identifier
                    );

                    events.AddEvent(new ValidationRuleFailure(errorMessage));
                }
            }
        }
    }
}
