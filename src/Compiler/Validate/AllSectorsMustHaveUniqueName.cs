using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllSectorsMustHaveUniqueName : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<Sector> duplicates = sectorElements.Sectors.GroupBy(sector => sector.Name) 
                .Where(group => group.Count() > 1)
                .Select(group => group.First())
                .ToList();

            foreach (Sector duplicate in duplicates)
            {
                string message = $"Duplicate SECTOR for {duplicate}";
                events.AddEvent(new ValidationRuleFailure(message, duplicate));
            }
        }
    }
}
