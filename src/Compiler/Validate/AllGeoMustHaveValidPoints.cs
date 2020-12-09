using System;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllGeoMustHaveValidPoints : IValidationRule
    {
        /**
         * This is a bit of an ES hack. SCT2 defines that all GEO has to be coordinates but ES
         * allows named fixes.
         */
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (Geo geo in sectorElements.GeoElements)
            {
                this.ValidateGeoSegment(geo.InitialSegment, sectorElements, events);
                foreach (GeoSegment segment in geo.AdditionalSegments)
                {
                    this.ValidateGeoSegment(segment, sectorElements, events);
                }
            }
        }

        private void ValidateGeoSegment(GeoSegment segment, SectorElementCollection sectorElements, IEventLogger events)
        {
            if (
                !RoutePointValidator.ValidatePoint(segment.FirstPoint, sectorElements) ||
                !RoutePointValidator.ValidatePoint(segment.SecondPoint, sectorElements)
            )
            {
                string message = String.Format(
                    "Invalid waypoint {0} on GEO",
                    segment.GetCompileData()
                );
                events.AddEvent(
                    new ValidationRuleFailure(message)
                );
            }
        }
    }
}
