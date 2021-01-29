using System.Linq;
using Compiler.Argument;
using Compiler.Error;
using Compiler.Event;
using Compiler.Model;

namespace Compiler.Validate
{
    public class AllActiveRunwaysMustReferenceARunway: IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            var missingRunways = sectorElements.ActiveRunways
                .Where(
                    activeRunway => !sectorElements.Runways.Exists(runway => IsSameRunway(activeRunway, runway))
                )
                .ToList();

            if (!missingRunways.Any())
            {
                return;
            }

            foreach (ActiveRunway runway in missingRunways)
            {
                events.AddEvent(
                    new ValidationRuleFailure(
                        $"Ruunway {runway.Airfield}/{runway.Identifier} specified in active runway does not exist",
                        runway
                    )
                );
            }
        }

        private bool IsSameRunway(ActiveRunway activeRunway, Runway runway)
        {
            return runway.AirfieldIcao == activeRunway.Airfield &&
                   (runway.FirstIdentifier == activeRunway.Identifier ||
                    runway.ReverseIdentifier == activeRunway.Identifier);
        }
    }
}