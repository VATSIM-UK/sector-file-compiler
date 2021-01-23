using System.Collections.Generic;
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
                        string message =
                            $"Invalid controlled sector {rule.ControlledSector} for CIRCLE_SECTORLINE display rule: {rule.GetCompileData(sectorElements)}";
                        events.AddEvent(new ValidationRuleFailure(message, rule));
                        continue;
                    }

                    if (!sectors.Contains(rule.CompareSectorFirst))
                    {
                        string message =
                            $"Invalid first compare sector {rule.CompareSectorFirst} for CIRCLE_SECTORLINE display rule: {rule.GetCompileData(sectorElements)}";
                        events.AddEvent(new ValidationRuleFailure(message, rule));
                        continue;
                    }

                    if (!sectors.Contains(rule.CompareSectorSecond))
                    {
                        string message =
                            $"Invalid second compare sector {rule.CompareSectorSecond} for CIRCLE_SECTORLINE display rule: {rule.GetCompileData(sectorElements)}";
                        events.AddEvent(new ValidationRuleFailure(message, rule));
                    }
                }
            }
        }
    }
}
