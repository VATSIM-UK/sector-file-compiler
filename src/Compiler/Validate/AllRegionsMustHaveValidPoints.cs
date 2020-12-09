using System;
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
                foreach (RegionPoint point in region.Points)
                {
                    this.CheckPoint(point, region, sectorElements, events);
                }
            }
        }

        public void CheckPoint(RegionPoint point, Region region, SectorElementCollection sectorElements, IEventLogger events)
        {
            if (!RoutePointValidator.ValidatePoint(point.Point, sectorElements))
            {
                string message = String.Format(
                    "Invalid waypoint {0} on region {1}",
                    point.Point.ToString(),
                    region.Name
                );
                events.AddEvent(
                    new ValidationRuleFailure(message)
                );
            }
        }
    }
}
