using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllSectorsMustHaveValidOwner : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> positions = sectorElements.EsePositions.Select(position => position.Identifier).ToList();
            foreach (Sector sector in sectorElements.Sectors)
            {
                foreach (string position in sector.Owners.Owners)
                {
                    if (!positions.Contains(position))
                    {
                        string message = $"Invalid OWNER position {position} on sector {sector.Name}";
                        events.AddEvent(new ValidationRuleFailure(message));
                        break;
                    }
                }
            }
        }
    }
}
