using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SidStarFactory
    {
        private static readonly string[] Identifiers = new[]
        {
            "BNN4A",
            "LOGAN1H",
            "LAM3X",
            "WOTAN1Z",
            "LISTO1S"
        };
        
        public static SidStar Make(bool isSid = true, string airport = null, string runway = null, string identifier = null, List<string> route = null)
        {   
            return new Faker<SidStar>()
                .CustomInstantiator(
                    f => new SidStar(
                        isSid ? "SID" : "STAR",
                        airport ?? AirportFactory.GetRandomDesignator(),
                        runway ?? RunwayFactory.GetRandomDesignator(),
                        identifier ?? f.Random.ArrayElement(Identifiers),
                        route ?? FixFactory.RandomIdentifiers(3),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
