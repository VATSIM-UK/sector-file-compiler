using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class InfoMustHaveValidAirport : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach(Airport airport in sectorElements.Airports)
            {
                if (airport.Icao == sectorElements.Info.Airport.AirportIcao)
                {
                    return;
                }
            }

            events.AddEvent(
                new ValidationRuleFailure("Invalid airport in INFO definition")
            );
        }       
    }
}
