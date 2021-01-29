using System.Linq;
using Compiler.Argument;
using Compiler.Error;
using Compiler.Event;
using Compiler.Model;

namespace Compiler.Validate
{
    public class AllActiveRunwaysMustReferenceAnAirport: IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            var airports = sectorElements.Airports.Select(airport => airport.Icao).ToList();
            var missingAirports = sectorElements.ActiveRunways
                .Where(activeRunway => !airports.Contains(activeRunway.Airfield));

            if (missingAirports.Count() == 0)
            {
                return;
            }

            foreach (ActiveRunway runway in missingAirports)
            {
                events.AddEvent(
                    new ValidationRuleFailure(
                        $"Airport {runway.Airfield} specified in active runway does not exist",
                        runway
                    )
                );
            }
        }
    }
}