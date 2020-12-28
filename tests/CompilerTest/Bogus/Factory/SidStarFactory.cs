using System.Runtime.CompilerServices;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SidStarFactory
    {
        private static readonly string[] identifiers = new[]
        {
            "BNN4A",
            "LOGAN1H",
            "LAM3X",
            "WOTAN1Z",
            "LISTO1S"
        };
        
        public static SidStar Make(bool isSid = true)
        {   
            return new Faker<SidStar>()
                .CustomInstantiator(
                    f => new SidStar(
                        isSid ? "SID" : "STAR",
                        AirportFactory.GetRandomDesignator(),
                        RunwayFactory.GetRandomDesignator(),
                        f.Random.ArrayElement(identifiers),
                        FixFactory.RandomIdentifiers(3),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
