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
    public class AllCoordinationPointsMustHaveValidToSector : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> sectors = sectorElements.Sectors.Select(sector => sector.Name).ToList();
            foreach (CoordinationPoint point in sectorElements.CoordinationPoints)
            {
                if (!sectors.Contains(point.ToSector))
                {
                    string message = String.Format(
                        "Invalid TO sector {0} for coordination point: {1}",
                        point.ToSector,
                        point.Compile()
                    );
                    events.AddEvent(new ValidationRuleFailure(message));
                    continue;
                }
            }
        }
    }
}
