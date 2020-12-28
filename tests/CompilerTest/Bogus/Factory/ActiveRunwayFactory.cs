using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class ActiveRunwayFactory
    {
        public static ActiveRunway Make(string airfieldIcao = null)
        {
            return new Faker<ActiveRunway>()
                .CustomInstantiator(
                    f => new ActiveRunway(
                        RunwayFactory.GetRandomDesignator(),
                        AirportFactory.GetRandomDesignator(),
                        f.Random.Bool() ? 1 : 0,
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
