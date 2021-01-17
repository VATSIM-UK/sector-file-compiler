using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllCoordinationPointsMustHaveValidFromSector : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> sectors = sectorElements.Sectors.Select(sector => sector.Name).ToList();
            foreach (CoordinationPoint point in sectorElements.CoordinationPoints)
            {
                if (!sectors.Contains(point.FromSector))
                {
                    string message =
                        $"Invalid FROM sector {point.FromSector} for coordination point: {point.GetCompileData(sectorElements)}";
                    events.AddEvent(new ValidationRuleFailure(message, point));
                }
            }
        }
    }
}
