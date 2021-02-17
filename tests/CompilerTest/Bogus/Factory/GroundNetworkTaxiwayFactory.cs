using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class GroundNetworkTaxiwayFactory
    {
        public static GroundNetworkTaxiway Make()
        {
            return new Faker<GroundNetworkTaxiway>()
                .CustomInstantiator(
                    _ => new GroundNetworkTaxiway(
                        "A",
                        15,
                        1,
                        "15",
                        new List<GroundNetworkCoordinate>
                        {
                            GroundNetworkCoordinateFactory.Make(),
                            GroundNetworkCoordinateFactory.Make(),
                            GroundNetworkCoordinateFactory.Make()
                        },
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
