using System;
using System.Linq;
using Compiler.Event;
using Compiler.Error;
using Compiler.Model;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllSidsMustHaveAValidAirport : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach(SidStar sidStar in sectorElements.SidStars)
            {
                bool airportExists = sectorElements.Airports
                    .Count(airport => sidStar.Airport == airport.Icao) != 0;

                if (!airportExists)
                {
                    string errorMessage =
                        $"Airport {sidStar.Airport} is not valid for {sidStar.Type}/{sidStar.Identifier}";

                    events.AddEvent(new ValidationRuleFailure(errorMessage));
                }
            }
        }
    }
}
