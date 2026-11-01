using System.Collections.Generic;
using System.Linq;
using Compiler.Argument;
using Compiler.Error;
using Compiler.Event;
using Compiler.Model;

namespace Compiler.Validate
{
    public class AllPositionsMustHaveUniqueIdentifier : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<List<ControllerPosition>> duplicates = sectorElements.EsePositions
                .GroupBy(position => position.Identifier.ToUpperInvariant())
                .Where(group => group.Count() > 1)
                .Select(group => group.ToList())
                .ToList();

            foreach (List<ControllerPosition> duplicate in duplicates)
            {
                events.AddEvent(
                    new ValidationRuleFailure(
                        $"Duplicate sector identifier '{duplicate[0].Identifier}' on positions " +
                        string.Join(
                            ", ",
                            duplicate.Select(position => $"{position.Callsign} ({position.GetDefinition()})")
                        ),
                        duplicate[0]
                    )
                );
            }
        }
    }
}
