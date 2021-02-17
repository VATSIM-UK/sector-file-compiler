using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class GroundNetworkCoordinateFactory
    {
        public static GroundNetworkCoordinate Make(Coordinate? coordinate = null)
        {
            return new Faker<GroundNetworkCoordinate>()
                .CustomInstantiator(
                    _ => new GroundNetworkCoordinate(
                        coordinate ?? CoordinateFactory.Make(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
