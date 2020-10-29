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
    public class AllCircleSectorlinesMustHaveValidDisplaySectors : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> sectors = sectorElements.Sectors.Select(sector => sector.Name).ToList();
            foreach (CircleSectorline sectorline in sectorElements.CircleSectorLines)
            {
                foreach (SectorlineDisplayRule rule in sectorline.DisplayRules)
                {
                    if (!sectors.Contains(rule.ControlledSector))
                    {
                        string message = String.Format(
                            "Invalid controlled sector {0} for CIRCLE_SECTORLINE display rule: {1}",
                            rule.ControlledSector,
                            rule.GetCompileData()
                        );
                        events.AddEvent(new ValidationRuleFailure(message));
                        continue;
                    }

                    if (!sectors.Contains(rule.CompareSectorFirst))
                    {
                        string message = String.Format(
                            "Invalid first compare sector {0} for CIRCLE_SECTORLINE display rule: {1}",
                            rule.CompareSectorFirst,
                            rule.GetCompileData()
                        );
                        events.AddEvent(new ValidationRuleFailure(message));
                        continue;
                    }

                    if (!sectors.Contains(rule.CompareSectorSecond))
                    {
                        string message = String.Format(
                            "Invalid second compare sector {0} for CIRCLE_SECTORLINE display rule: {1}",
                            rule.CompareSectorSecond,
                            rule.Compile()
                        );
                        events.AddEvent(new ValidationRuleFailure(message));
                        continue;
                    }
                }
            }
        }
    }
}
