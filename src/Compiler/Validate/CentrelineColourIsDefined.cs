using System.Linq;
using Compiler.Argument;
using Compiler.Error;
using Compiler.Event;
using Compiler.Model;

namespace Compiler.Validate
{
    public class CentrelineColourIsDefined: IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            if (!sectorElements.Colours.Any(colour => colour.Name == "centrelinecolour"))
            {
                events.AddEvent(
                    new ValidationRuleFailure(
                        "Runway centreline colour is not defined",
                        sectorElements.FixedColourRunwayCentrelines[0]
                    )
                );
            }
        }
    }
}