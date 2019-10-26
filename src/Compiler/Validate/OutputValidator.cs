using System.Collections.Generic;
using Compiler.Model;
using Compiler.Event;

namespace Compiler.Validate
{
    public class OutputValidator
    {
        private static List<IValidationRule> validationRules = new List<IValidationRule>
        {
            new AllSidsMustBeUnique(),
        };

        public static void Validate(SectorElementCollection sectorElements, IEventLogger events)
        {
            foreach (IValidationRule rule in OutputValidator.validationRules)
            {
                rule.Validate(sectorElements, events);
            }
        }
    }
}
