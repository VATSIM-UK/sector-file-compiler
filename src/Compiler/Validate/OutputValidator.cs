﻿using System.Collections.Generic;
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
