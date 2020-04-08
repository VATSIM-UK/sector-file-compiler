using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllRegionsMustHaveValidPoints : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (Region region in sectorElements.Regions)
            {
                foreach (Point point in region.Points)
                {
                    if (!RoutePointValidator.ValidatePoint(point, sectorElements))
                    {
                        string message = String.Format(
                            "Invalid waypoint {0} on region {1}",
                            point.Compile(),
                            region.Colour
                        );
                        events.AddEvent(
                            new ValidationRuleFailure(message)
                        );
                    }
                }
            }
        }
    }
}
