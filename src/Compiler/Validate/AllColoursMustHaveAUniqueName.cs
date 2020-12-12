using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllColoursMustHaveAUniqueId : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> coloursProcessed = new List<string>();
            foreach (Colour colour in sectorElements.Colours)
            {
                if (coloursProcessed.Contains(colour.Name.ToLower()))
                {
                    events.AddEvent(new ValidationRuleFailure("Duplicate colourm definition " + colour.Name));
                    continue;
                }

                coloursProcessed.Add(colour.Name.ToLower());
            }
        }
    }
}
