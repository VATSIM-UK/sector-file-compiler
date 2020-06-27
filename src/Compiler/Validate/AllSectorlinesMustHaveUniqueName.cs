using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllSectorlinesMustHaveUniqueName : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {

            List<string> duplicates = sectorElements.SectorLines.GroupBy(sectorline => sectorline.Name) 
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToList();

            foreach (string duplicate in duplicates)
            {
                string message = String.Format(
                    "Duplicate SECTORLINE for {0}",
                    duplicate
                );
                events.AddEvent(new ValidationRuleFailure(message));
                continue;
            }
        }
    }
}
