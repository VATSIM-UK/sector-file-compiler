using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllSectorsBordersMustBeSingleIfCircle : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            Dictionary<string, CircleSectorline> sectorlines = sectorElements.CircleSectorLines.ToDictionary(
                sectorline => sectorline.Name,
                sectorline => sectorline
            );

            foreach (Sector sector in sectorElements.Sectors)
            {
                foreach (SectorBorder border in sector.Borders)
                {
                    var circleBorders = border.BorderLines.Where(
                        borderLine => sectorlines.ContainsKey(borderLine)
                    ).ToList();

                    if (circleBorders.Count > 0 && border.BorderLines.Count > 1)
                    {
                        events.AddEvent(new ValidationRuleFailure(ErrorMessage(sector), border));
                        break;
                    }
                }
            }
        }

        private string ErrorMessage(Sector sector)
        {
            return
                $"SECTOR {sector.Name} has a BORDER with a CIRCLELINE, but contains more than one element";
        }
    }
}
