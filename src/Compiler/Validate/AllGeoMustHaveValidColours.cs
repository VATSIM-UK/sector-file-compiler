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
                if (!ColourValidator.ColourValid(sectorElements, geo.Colour))
                {
                    string errorMessage = string.Format(
                        "Invalid colour value {0} in GEO segment",
                        geo.Colour
                    );
                    events.AddEvent(new ValidationRuleFailure(errorMessage));
                    continue;
                }
            }
        }
    }
}
