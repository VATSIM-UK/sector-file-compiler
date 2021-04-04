using System.Collections.Generic;

namespace Compiler.Model
{
    /**
     * Container for all the ground network elements related to an airport
     */
    public class GroundNetwork: ICompilableElementProvider
    {
        public string Airport { get; }
        public List<GroundNetworkTaxiway> Taxiways { get; }
        public List<GroundNetworkRunwayExit> RunwayExits { get; }

        public GroundNetwork(
            string airport,
            List<GroundNetworkTaxiway> taxiways,
            List<GroundNetworkRunwayExit> runwayExits
        )
        {
            Airport = airport;
            Taxiways = taxiways;
            RunwayExits = runwayExits;
        }

        public IEnumerable<ICompilableElement> GetCompilableElements()
        {
            List<ICompilableElement> values = new();
            Taxiways.ForEach(taxiway => values.AddRange(taxiway.GetCompilableElements()));
            RunwayExits.ForEach(exit => values.AddRange(exit.GetCompilableElements()));
            return values;
        }
    }
}
