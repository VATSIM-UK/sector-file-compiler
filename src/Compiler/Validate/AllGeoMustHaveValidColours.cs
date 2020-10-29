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
                this.ValidateGeoSegment(geo.InitialSegment, sectorElements, events);
                foreach (GeoSegment segment in geo.AdditionalSegments)
                {
                    this.ValidateGeoSegment(segment, sectorElements, events);
                }
            }
        }

        private void ValidateGeoSegment(GeoSegment segment, SectorElementCollection sectorElements, IEventLogger events)
        {
            if (!ColourValidator.ColourValid(sectorElements, segment.Colour))
            {
                string errorMessage = string.Format(
                    "Invalid colour value {0} in GEO segment {1}",
                    segment.Colour,
                    segment.GetCompileData()
                );
                events.AddEvent(new ValidationRuleFailure(errorMessage));
            }
        }
    }
}
