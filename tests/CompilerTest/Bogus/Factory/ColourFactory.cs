using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class ColourFactory
    {
        private static readonly string[] Identifiers = new[] {
            "red",
            "white",
            "smrGDGround",
            "blue"
        };

        public static Colour Make(string identifier = null, int? value = null)
        {
            return new Faker<Colour>()
                .CustomInstantiator(
                    f => new Colour(
                        identifier ?? f.Random.ArrayElement(Identifiers),
                        value ?? f.Random.Int(0, 16777215),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }

        public static string RandomIdentifier()
        {
            return new Randomizer().ArrayElement(Identifiers);
        }
    }
}
