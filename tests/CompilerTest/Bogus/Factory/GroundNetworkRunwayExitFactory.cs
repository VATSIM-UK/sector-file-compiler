using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class GroundNetworkRunwayExitFactory
    {
        public static GroundNetworkRunwayExit Make()
        {
            return new Faker<GroundNetworkRunwayExit>()
                .CustomInstantiator(
                    _ => new GroundNetworkRunwayExit(
                        "27L",
                        "N3W",
                        11,
                        15,
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
