using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class GroundNetworkFactory
    {
        public static GroundNetwork Make(string airfield = "EGLL")
        {
            return new Faker<GroundNetwork>()
                .CustomInstantiator(
                    _ => new GroundNetwork(
                        airfield,
                        new List<GroundNetworkTaxiway>
                        {
                            GroundNetworkTaxiwayFactory.Make(),
                            GroundNetworkTaxiwayFactory.Make(),
                            GroundNetworkTaxiwayFactory.Make()
                        },
                        new List<GroundNetworkRunwayExit>
                        {
                            GroundNetworkRunwayExitFactory.Make(),
                            GroundNetworkRunwayExitFactory.Make(),
                            GroundNetworkRunwayExitFactory.Make()
                        }
                    )
                );
        }
    }
}
