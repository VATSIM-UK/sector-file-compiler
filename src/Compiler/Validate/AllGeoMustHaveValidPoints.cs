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
                ValidateBaseGeo(geo, sectorElements, events);
                foreach (GeoSegment segment in geo.AdditionalSegments)
                {
                    ValidateGeoSegment(segment, sectorElements, events);
                }
            }
        }

        private static void ValidateBaseGeo(Geo geo, SectorElementCollection sectorElements, IEventLogger events)
        {
            if (!PointsValid(geo.FirstPoint, geo.SecondPoint, sectorElements))
            {
                string message = $"Invalid waypoint on GEO declaration: {geo.GetCompileData(sectorElements)}";
                events.AddEvent(
                    new ValidationRuleFailure(message, geo)
                );
            }
        }

        private static void ValidateGeoSegment(GeoSegment segment, SectorElementCollection sectorElements, IEventLogger events)
        {
            if (!PointsValid(segment.FirstPoint, segment.SecondPoint, sectorElements))
            {
                string message = $"Invalid waypoint on GEO segment: {segment.GetCompileData(sectorElements)}";
                events.AddEvent(
                    new ValidationRuleFailure(message, segment)
                );
            }
        }

        private static bool PointsValid(Point point1, Point point2, SectorElementCollection sectorElements)
        {
            return RoutePointValidator.ValidatePoint(point1, sectorElements) && 
                   RoutePointValidator.ValidatePoint(point2, sectorElements);
        }
    }
}
