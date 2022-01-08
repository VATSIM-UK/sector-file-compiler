using System.Collections.Generic;
using System.Threading.Tasks;
using Compiler.Model;
using Compiler.Event;
using Compiler.Argument;

namespace Compiler.Validate
{
    public class OutputValidator
    {
        private static readonly List<IValidationRule> ValidationRules = new()
        {
            new AllAirportsMustHaveUniqueCode(),
            new AllAirwaysMustHaveValidPoints(),
            new AllArtccsMustHaveValidPoints(),
            new AllColoursMustBeValid(),
            new AllColoursMustHaveAUniqueId(),
            new AllFixesMustBeUnique(),
            new AllSidsMustBeUnique(),
            new AllSidsMustHaveAValidRoute(),
            new AllSctSidsMustHaveAValidRoute(),
            new AllSctStarsMustHaveAValidRoute(),
            new AllSctSidsMustHaveValidColours(),
            new AllSctStarsMustHaveValidColours(),
            new AllGeoMustHaveValidColours(),
            new AllGeoMustHaveValidPoints(),
            new AllLabelsMustHaveAValidColour(),
            new AllRegionsMustHaveValidColours(),
            new AllRegionsMustHaveValidPoints(),
            new InfoMustHaveValidAirport(),
            new AllCoordinationPointsMustHaveValidPrior(),
            new AllCoordinationPointsMustHaveValidNext(),
            new AllCoordinationPointsMustHaveValidFix(),
            new AllCoordinationPointsMustHaveValidToSector(),
            new AllCoordinationPointsMustHaveValidFromSector(),
            new AllSectorlineElementsMustHaveUniqueName(),
            new AllSectorlinesMustHaveValidDisplaySectors(),
            new AllCircleSectorlinesMustHaveValidDisplaySectors(),
            new AllSectorlineElementsMustHaveUniqueName(),
            new AllSectorsMustHaveValidOwner(),
            new AllSectorsMustHaveValidAltOwner(),
            new AllSectorsMustHaveValidBorder(),
            new AllSectorsBordersMustBeSingleIfCircle(),
            new AllSectorBordersMustBeContiguous(),
            new AllSectorsMustHaveValidActiveAirport(),
            new AllSectorsMustHaveValidActiveRunway(),
            new AllSectorsMustHaveValidGuestAirports(),
            new AllSectorsMustHaveValidGuestController(),
            new AllSectorsMustHaveValidDepartureAirports(),
            new AllSectorsMustHaveValidArrivalAirports(),
            new AllRunwaysMustReferenceAnAirport(),
            new AllCoordinationPointsMustHaveValidDepartureRunways(),
            new AllCoordinationPointsMustHaveValidArrivalRunways(),
            new CentrelineColourIsDefined(),
            new AllActiveRunwaysMustReferenceAnAirport(),
            new AllActiveRunwaysMustReferenceARunway(),
            new AllActiveRunwaysMustBeUnique(),
            new AllRunwayExitsMustHaveAValidRunway(),
            new OwnersMayOnlyAppearOnceInSectorOwnership(),
            new AltOwnersMayOnlyAppearOnceInEachAltOwnershipLine(),
            new AllSidsMustHaveAValidRunway()
        };

        public static void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            var tasks = new List<Task>();
            foreach (IValidationRule rule in ValidationRules)
            {
                tasks.Add(RunValidationTask(rule, sectorElements, args, events));
            }

            Task.WaitAll(tasks.ToArray());
        }

        private static Task RunValidationTask(
            IValidationRule rule,
            SectorElementCollection sectorElements,
            CompilerArguments args,
            IEventLogger eventLogger
        ) {
            return Task.Factory.StartNew(() => rule.Validate(sectorElements, args, eventLogger));
        }
    }
}
