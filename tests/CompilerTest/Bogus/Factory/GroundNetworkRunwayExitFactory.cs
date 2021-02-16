using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class GroundNetworkRunwayExitFactory
    {
        public static GroundNetworkRunwayExit Make(string runway = null)
        {
            return new Faker<GroundNetworkRunwayExit>()
                .CustomInstantiator(
                    faker => new GroundNetworkRunwayExit(
                        runway ?? "27L",
                        "N3W",
                        faker.Random.ArrayElement(new []{"LEFT", "RIGHT"}),
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
