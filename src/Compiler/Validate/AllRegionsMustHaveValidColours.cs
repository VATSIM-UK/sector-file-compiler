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
                foreach (RegionPoint point in region.Points)
                {
                    if (!ColourValidator.ColourValid(sectorElements, point.Colour))
                    {
                        string errorMessage = string.Format(
                            "Invalid colour value {0} for region {1}",
                            point.Colour,
                            region.Name
                        );
                        events.AddEvent(new ValidationRuleFailure(errorMessage));
                        continue;
                    }
                }
            }
        }
    }
}
