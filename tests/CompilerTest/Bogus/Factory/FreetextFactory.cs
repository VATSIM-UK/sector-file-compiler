using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class FreetextFactory
    {
        public static Freetext Make(Definition definition = null)
        {
            return new Faker<Freetext>()
                .CustomInstantiator(
                    f => new Freetext(
                        "Test Title",
                        "Test Freetext",
                        CoordinateFactory.Make(),
                        definition ?? DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
