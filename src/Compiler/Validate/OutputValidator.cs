using System.Collections.Generic;
using Compiler.Model;
using Compiler.Event;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class OutputValidator
    {
        private static readonly List<IValidationRule> validationRules = new List<IValidationRule>
        {
            new AllAirportsMustHaveUniqueCode(),
            new AllAirwaysMustHaveValidPoints(),
            new AllArtccsMustHaveValidPoints(),
            new AllColoursMustBeValid(),
            new AllColoursMustHaveAUniqueId(),
            new AllFixesMustBeUnique(),
            new AllSidsMustBeUnique(),
            new AllSidsMustHaveAValidAirport(),
            new AllSidsMustHaveAValidRoute(),
            new AllSctSidsMustHaveAValidRoute(),
            new AllSctStarsMustHaveAValidRoute(),
            new AllSctSidsMustHaveContiguousRoute(),
            new AllSctStarsMustHaveContiguousRoute(),
            new AllSctSidsMustHaveValidColours(),
            new AllSctStarsMustHaveValidColours(),
            new AllGeoMustHaveValidColours(),
            new AllGeoMustHaveValidPoints(),
            new AllLabelsMustHaveAValidColour(),
            new AllRegionsMustHaveValidColours(),
            new AllRegionsMustHaveValidPoints(),
            new InfoMustHaveValidAirport(),
            new AllCoordinationPointsMustHaveValidPrior(),
        };

        public static void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (IValidationRule rule in OutputValidator.validationRules)
            {
                rule.Validate(sectorElements, args, events);
            }
        }
    }
}
