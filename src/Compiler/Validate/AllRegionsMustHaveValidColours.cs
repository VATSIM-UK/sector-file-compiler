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
                    if (point.Colour != null && !ColourValidator.ColourValid(sectorElements, point.Colour))
                    {
                        string errorMessage = $"Invalid colour value {point.Colour} for region {region.Name}";
                        events.AddEvent(new ValidationRuleFailure(errorMessage, point));
                    }
                }
            }
        }
    }
}
