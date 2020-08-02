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
                foreach (GeoSegment segment in geo.Segments)
                {
                    if (!ColourValidator.ColourValid(sectorElements, segment.Colour))
                    {
                        string errorMessage = string.Format(
                            "Invalid colour value {0} in GEO segment",
                            segment.Colour
                        );
                        events.AddEvent(new ValidationRuleFailure(errorMessage));
                    }
                }
            }
        }
    }
}
