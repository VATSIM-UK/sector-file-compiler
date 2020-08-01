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
    public class AllCoordinationPointsMustHaveValidArrivalRunways : AbstractCoordinationPointRunwayChecker, IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (CoordinationPoint point in sectorElements.CoordinationPoints)
            {
                if (!RunwayValid(sectorElements, point.ArrivalRunway, point.ArrivalAiportOrFixAfter)) {
                    string message = String.Format(
                        "Invalid arrival runway {0}/{1} for coordination point: {1}",
                        point.ArrivalRunway,
                        point.ArrivalAiportOrFixAfter,
                        point.Compile()
                    );
                    events.AddEvent(new ValidationRuleFailure(message));
                    continue;
                }
            }
        }
    }
}
