using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class AirportFactory
    {
        private static readonly string[] designators = new[] {
            "EGLL",
            "LXGB",
            "EGSS",
            "EGPH",
            "EIDW",
            "EHAM",
            "EGGD",
            "EGJJ"
        };

        public static Airport Make(string icao = null)
        {
            return new Faker<Airport>()
                .CustomInstantiator(
                    f => new Airport(
                        f.Random.String2(10),
                        icao ?? GetRandomDesignator(),
                        CoordinateFactory.Make(),
                        "123.450",
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )    
                );
        }
        
        public static string GetRandomDesignator()
        {
            return new Randomizer().ArrayElement(designators);
        }

        public static List<string> GetListOfDesignators()
        {
            return new Randomizer().ArrayElements(designators).ToList();
        }
    }
}
