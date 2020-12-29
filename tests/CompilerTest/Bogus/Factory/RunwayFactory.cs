using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class RunwayFactory
    {
        private static readonly string[] designators = new[] {
            "09L",
            "23R",
            "01",
            "36",
            "09",
            "27",
            "02G",
            "31"
        };
        
        public static string GetRandomDesignator()
        {
            return new Randomizer().ArrayElement(designators);
        }

        public static Runway Make(string airfieldIcao = null, string designator1 = null, string designator2 = null)
        {
            return new Faker<Runway>()
                .CustomInstantiator(
                    f => new Runway(
                        airfieldIcao ?? AirportFactory.GetRandomDesignator(),
                        designator1 ?? f.Random.ArrayElement(designators),
                        000,
                        CoordinateFactory.Make(),
                        designator2 ?? f.Random.ArrayElement(designators),
                        000,
                        CoordinateFactory.Make(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
