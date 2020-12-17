using System;
using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllSectorlinesMustHaveValidDisplaySectors : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> sectors = sectorElements.Sectors.Select(sector => sector.Name).ToList();
            foreach (Sectorline sectorline in sectorElements.SectorLines)
            {
                foreach (SectorlineDisplayRule rule in sectorline.DisplayRules)
                {
                    if (!sectors.Contains(rule.ControlledSector))
                    {
                        string message = String.Format(
                            "Invalid controlled sector {0} for SECTORLINE display rule: {1}",
                            rule.ControlledSector,
                            rule.GetCompileData(sectorElements)
                        );
                        events.AddEvent(new ValidationRuleFailure(message));
                        continue;
                    }

                    if (!sectors.Contains(rule.CompareSectorFirst))
                    {
                        string message = String.Format(
                            "Invalid first compare sector {0} for SECTORLINE display rule: {1}",
                            rule.CompareSectorFirst,
                            rule.GetCompileData(sectorElements)
                        );
                        events.AddEvent(new ValidationRuleFailure(message));
                        continue;
                    }

                    if (!sectors.Contains(rule.CompareSectorSecond))
                    {
                        string message = String.Format(
                            "Invalid second compare sector {0} for SECTORLINE display rule: {1}",
                            rule.CompareSectorSecond,
                            rule.GetCompileData(sectorElements)
                        );
                        events.AddEvent(new ValidationRuleFailure(message));
                        continue;
                    }
                }
            }
        }
    }
}
