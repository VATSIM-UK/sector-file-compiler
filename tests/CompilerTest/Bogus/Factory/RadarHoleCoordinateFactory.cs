using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class RadarHoleCoordinateFactory
    {
        public static RadarHoleCoordinate Make()
        {
            return new Faker<RadarHoleCoordinate>()
                .CustomInstantiator(
                    _ => new RadarHoleCoordinate(
                        CoordinateFactory.Make(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
