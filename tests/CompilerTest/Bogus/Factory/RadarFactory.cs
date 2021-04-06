using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class RadarFactory
    {
        public static Radar Make()
        {
            return new Faker<Radar>()
                .CustomInstantiator(
                    f => new Radar(
                        f.Random.Word(),
                        CoordinateFactory.Make(),
                        RadarParametersFactory.Make(), 
                        RadarParametersFactory.Make(), 
                        RadarParametersFactory.Make(), 
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
