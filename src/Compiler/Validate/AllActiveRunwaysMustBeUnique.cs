using System.Linq;
using Compiler.Argument;
using Compiler.Error;
using Compiler.Event;
using Compiler.Model;

namespace Compiler.Validate
{
    public class AllActiveRunwaysMustBeUnique: IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            var duplicates = sectorElements.ActiveRunways.GroupBy(
                    runway => runway
                )
                .Where(g => g.Count() > 1)
                .ToList();

            if (!duplicates.Any())
            {
                return;
            }

            foreach (var duplicate in duplicates)
            {
                ActiveRunway runway = duplicate.First();
                events.AddEvent(
                    new ValidationRuleFailure(
                        $"Duplicate ACTIVE_RUNWAY {runway.Airfield}:{runway.Identifier}:{runway.Mode}",
                        runway
                    )
                );
            }
        }
    }
}