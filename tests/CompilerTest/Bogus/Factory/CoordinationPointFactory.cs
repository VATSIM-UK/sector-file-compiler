using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class CoordinationPointFactory
    {
        public static CoordinationPoint Make(bool firCopx = false)
        {
            return new Faker<CoordinationPoint>()
                .CustomInstantiator(
                    f => new CoordinationPoint(
                        firCopx,
                        FixFactory.RandomIdentifier(),
                        "",
                        FixFactory.RandomIdentifier(),
                        FixFactory.RandomIdentifier(),
                        "",
                        "AB",
                        "CD",
                        "6000",
                        null,
                        "TEST",
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
