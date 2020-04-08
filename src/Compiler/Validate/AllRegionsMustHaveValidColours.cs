using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllRegionsMustHaveValidColours : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (Region region in sectorElements.Regions)
            {
                if (!ColourValidator.ColourValid(sectorElements, region.Colour))
                {
                    string errorMessage = string.Format(
                        "Invalid colour value {0} for region",
                        region.Colour
                    );
                    events.AddEvent(new ValidationRuleFailure(errorMessage));
                    continue;
                }
            }
        }
    }
}
