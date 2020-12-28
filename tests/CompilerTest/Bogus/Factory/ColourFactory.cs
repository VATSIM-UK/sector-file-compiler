using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class ColourFactory
    {
        private static readonly string[] identifiers = new[] {
            "red",
            "white",
            "smrGDGround",
            "blue"
        };

        public static Colour Make(string identifier = null)
        {
            return new Faker<Colour>()
                .CustomInstantiator(
                    f => new Colour(
                        identifier ?? f.Random.ArrayElement(identifiers),
                        f.Random.Int(0, 16777215),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }

        public static string RandomIdentifier()
        {
            return new Randomizer().ArrayElement(identifiers);
        }
    }
}
