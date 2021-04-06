using System.Linq;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class OwnersMayOnlyAppearOnceInSectorOwnership : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            var duplicates = sectorElements.Sectors.Where(
                sector => sector.Owners.Owners.GroupBy(owner => owner).Any(g => g.Count() > 1)
            );
            
            foreach (Sector withDuplicates in duplicates)
            {
                var duplicateOwners = withDuplicates.Owners.Owners.GroupBy(owner => owner)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.First());

                foreach (string duplicateOwner in duplicateOwners)
                {
                    events.AddEvent(
                        new ValidationRuleFailure($"Duplicate OWNER {duplicateOwner} in SECTOR", withDuplicates.Owners)
                    );
                }
            }
        }
    }
}
