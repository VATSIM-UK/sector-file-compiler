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
    public class AllSectorsMustHaveValidActiveAirport : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> airports = sectorElements.Airports.Select(airport => airport.Icao).ToList();
            airports.Add("000A");
            foreach (Sector sector in sectorElements.Sectors)
            {
                foreach (SectorActive active in sector.Active)
                {
                    if (!airports.Contains(active.Airfield))
                    {
                        string message = String.Format(
                            "Invalid ACTIVE airport {0} on sector {1}",
                            active.Airfield,
                            sector.Name
                        );
                        events.AddEvent(new ValidationRuleFailure(message));
                        break;
                    }
                }
            }
        }
    }
}
