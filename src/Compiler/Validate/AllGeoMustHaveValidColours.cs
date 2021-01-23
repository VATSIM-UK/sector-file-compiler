using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllGeoMustHaveValidColours : IValidationRule
    {
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
            if (!ColourValid(geo.Colour, sectorElements))
            {
                string errorMessage =
                    $"Invalid colour value {geo.Colour} in GEO declaration {geo.GetCompileData(sectorElements)}";
                events.AddEvent(new ValidationRuleFailure(errorMessage, geo));
            }
        }

        private static void ValidateGeoSegment(GeoSegment segment, SectorElementCollection sectorElements, IEventLogger events)
        {
            if (!ColourValid(segment.Colour, sectorElements))
            {
                string errorMessage =
                    $"Invalid colour value {segment.Colour} in GEO segment {segment.GetCompileData(sectorElements)}";
                events.AddEvent(new ValidationRuleFailure(errorMessage, segment));
            }
        }

        private static bool ColourValid(string colour, SectorElementCollection sectorElements)
        {
            return colour == null || ColourValidator.ColourValid(sectorElements, colour);
        }
    }
}
