using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class ActiveRunwayFactory
    {
        public static ActiveRunway Make(string airfieldIcao = null, string runwayDesignator = null, int? mode = null)
        {
            return new Faker<ActiveRunway>()
                .CustomInstantiator(
                    f => new ActiveRunway(
                        runwayDesignator ?? RunwayFactory.GetRandomDesignator(),
                        airfieldIcao ?? AirportFactory.GetRandomDesignator(),
                        mode ?? (f.Random.Bool() ? 1 : 0),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
