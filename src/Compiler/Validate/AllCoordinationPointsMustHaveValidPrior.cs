﻿using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllCoordinationPointsMustHaveValidPrior : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            List<string> airports = sectorElements.Airports.Select(airport => airport.Icao).ToList();
            List<string> fixes = sectorElements.Fixes.Select(fix => fix.Identifier).ToList();
            List<string> vors = sectorElements.Vors.Select(vor => vor.Identifier).ToList();
            List<string> ndbs = sectorElements.Ndbs.Select(ndb => ndb.Identifier).ToList();
            foreach (CoordinationPoint point in sectorElements.CoordinationPoints)
            {
                if (
                    point.DepartureAirportOrFixBefore != "*" &&
                    !airports.Contains(point.DepartureAirportOrFixBefore) &&
                    !fixes.Contains(point.DepartureAirportOrFixBefore) &&
                    !vors.Contains(point.DepartureAirportOrFixBefore) &&
                    !ndbs.Contains(point.DepartureAirportOrFixBefore)
                ) {
                    string message = String.Format(
                        "Invalid previous fix or departure airport {0} on coordination point: {1}",
                        point.DepartureAirportOrFixBefore,
                        point.Compile()
                    );
                    events.AddEvent(new ValidationRuleFailure(message));
                    continue;
                }
            }
        }
    }
}