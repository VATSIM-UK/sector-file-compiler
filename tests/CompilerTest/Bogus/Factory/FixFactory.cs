using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class FixFactory
    {
        private static readonly string[] Identifiers = new[] {
            "DIKAS",
            "PENIL",
            "UPGAS",
            "MONTY",
            "KONAN",
            "PEDIG",
            "NORBO",
            "ATSIX"
        };

        public static Fix Make(string identifier = null, Coordinate? coordinate = null)
        {
            return new Faker<Fix>()
                .CustomInstantiator(
                    f => new Fix(
                        identifier ?? f.Random.ArrayElement(Identifiers),
                        coordinate ?? CoordinateFactory.Make(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }

        public static List<string> RandomIdentifiers(int count = 1)
        {
            return new Randomizer().ArrayElements(Identifiers, count).ToList();
        }

        public static string RandomIdentifier()
        {
            return RandomIdentifiers().ElementAt(0);
        }
    }
}
