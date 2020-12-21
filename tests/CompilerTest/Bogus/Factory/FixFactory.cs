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
    }
}
