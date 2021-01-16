using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllSectorsMustHaveValidGuestController : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> controllers = sectorElements.EsePositions.Select(position => position.Identifier).ToList();
            foreach (Sector sector in sectorElements.Sectors)
            {
                foreach (SectorGuest guest in sector.Guests)
                {
                    if (!controllers.Contains(guest.Controller))
                    {
                        string message = $"Invalid GUEST position {guest.Controller} on sector {sector.Name}";
                        events.AddEvent(new ValidationRuleFailure(message));
                        break;
                    }
                }
            }
        }
    }
}
