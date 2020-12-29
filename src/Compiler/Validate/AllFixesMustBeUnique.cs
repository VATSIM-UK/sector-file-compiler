using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllFixesMustBeUnique : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> checkedFixes = new List<string>();
            foreach (Fix fix in sectorElements.Fixes)
            {
                if (checkedFixes.Contains(GetFixKey(fix)))
                {
                    events.AddEvent(new ValidationRuleFailure("Duplicate fix found: " + fix.Identifier));
                    continue;
                }

                checkedFixes.Add(GetFixKey(fix));           
            }
        }

        private static string GetFixKey(Fix fix)
        {
            return fix.Identifier + "." + fix.Coordinate.ToString();
        }
    }
}
