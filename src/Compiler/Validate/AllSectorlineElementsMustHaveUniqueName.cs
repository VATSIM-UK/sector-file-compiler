using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllSectorlineElementsMustHaveUniqueName : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            var duplicates = sectorElements.SectorLines.ToDictionary(
                    element => (AbstractCompilableElement) element, element => element.Name
                )
                .Concat(
                    sectorElements.CircleSectorLines.ToDictionary(
                        element => (AbstractCompilableElement) element, element => element.Name
                    )
                )
                .GroupBy(pair => pair.Value)
                .Where(group => group.Count() > 1)
                .Select(group => group.First())
                .ToList();

            foreach (var duplicate in duplicates)
            {
                string message = $"Duplicate SECTORLINE or CIRCLE_SECTORLINE for {duplicate.Value}";
                events.AddEvent(new ValidationRuleFailure(message, duplicate.Key));
            }
        }
    }
}
