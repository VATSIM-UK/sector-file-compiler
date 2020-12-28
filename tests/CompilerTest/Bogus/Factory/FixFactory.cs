using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class FixFactory
    {
        private static readonly string[] identifiers = new[] {
            "DIKAS",
            "PENIL",
            "UPGAS",
            "MONTY",
            "KONAN",
            "PEDIG",
            "NORBO",
            "ATSIX"
        };

        public static Fix Make(string identifier = null)
        {
            return new Faker<Fix>()
                .CustomInstantiator(
                    f => new Fix(
                        identifier ?? f.Random.ArrayElement(identifiers),
                        CoordinateFactory.Make(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }

        public static List<string> RandomIdentifiers(int count = 1)
        {
            return new Randomizer().ArrayElements(identifiers, count).ToList();
        }

        public static string RandomIdentifier()
        {
            return RandomIdentifiers().ElementAt(0);
        }
    }
}
