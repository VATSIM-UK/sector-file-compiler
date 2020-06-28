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
    public class AllSectorsMustHaveValidBorder : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> circleSectorlines = sectorElements.CircleSectorLines.Select(line => line.Name).ToList();
            List<string> sectorlines = sectorElements.SectorLines.Select(line => line.Name).ToList();
            foreach (Sector sector in sectorElements.Sectors)
            {
                foreach (string position in sector.Border.BorderLines)
                {
                    if (!circleSectorlines.Contains(position) && !sectorlines.Contains(position))
                    {
                        string message = String.Format(
                            "Invalid BORDER line {0} on sector {1}",
                            position,
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
