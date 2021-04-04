using System.Linq;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AltOwnersMayOnlyAppearOnceInEachAltOwnershipLine : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            var duplicates = sectorElements.Sectors.Where(
                sector => sector.AltOwners.Any(altowners => altowners.Owners.GroupBy(owner => owner).Any(g => g.Count() > 1))
            );
            
            foreach (Sector withDuplicates in duplicates)
            {
                foreach (SectorAlternateOwnerHierarchy alternateOwnerHierarchy in withDuplicates.AltOwners)
                {
                    var duplicateAltowners = alternateOwnerHierarchy.Owners.GroupBy(owner => owner)
                        .Where(g => g.Count() > 1)
                        .Select(g => g.First());

                    foreach (string duplicateAltOwner in duplicateAltowners)
                    {
                        events.AddEvent(
                            new ValidationRuleFailure($"Duplicate ALTOWNER {duplicateAltOwner} in SECTOR", alternateOwnerHierarchy)
                        );
                    }
                }
            }
        }
    }
}
