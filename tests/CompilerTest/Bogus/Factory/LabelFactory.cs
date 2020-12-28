using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class LabelFactory
    {
        public static Label Make(string identifier = null)
        {
            return new Faker<Label>()
                .CustomInstantiator(
                    f => new Label(
                        "Test Label",
                        CoordinateFactory.Make(),
                        ColourFactory.RandomIdentifier(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
