using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllSectorsMustHaveValidBorder : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> circleSectorlines = sectorElements.CircleSectorLines.Select(line => line.Name).ToList();
            List<string> sectorlines = sectorElements.SectorLines.Select(line => line.Name).ToList();
            foreach (Sector sector in sectorElements.Sectors)
            {
                foreach (SectorBorder border in sector.Borders)
                {
                    foreach (string borderLine in border.BorderLines)
                    {
                        if (!circleSectorlines.Contains(borderLine) && !sectorlines.Contains(borderLine))
                        {
                            string message = $"Invalid BORDER line {borderLine} on sector {sector.Name}";
                            events.AddEvent(new ValidationRuleFailure(message));
                            break;
                        }
                    }
                }
            }
        }
    }
}
