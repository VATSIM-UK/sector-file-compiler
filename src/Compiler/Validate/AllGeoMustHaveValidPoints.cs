using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class AllGeoMustHaveValidPoints : IValidationRule
    {
        /**
         * This is a bit of an ES hack. SCT2 defines that all GEO has to be coordinates but ES
         * allows named fixes.
         */
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            foreach (Geo geo in sectorElements.GeoElements)
            {
                if (
                    !RoutePointValidator.ValidatePoint(geo.StartPoint, sectorElements) ||
                    !RoutePointValidator.ValidatePoint(geo.EndPoint, sectorElements)
                )
                {
                    string message = String.Format(
                        "Invalid waypoint {0} on GEO",
                        geo.Compile()
                    );
                    events.AddEvent(
                        new ValidationRuleFailure(message)
                    );
                }
            }
        }
    }
}
