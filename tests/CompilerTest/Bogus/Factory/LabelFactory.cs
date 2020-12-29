using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class LabelFactory
    {
        public static Label Make(string text = null, string colour = null)
        {
            return new Faker<Label>()
                .CustomInstantiator(
                    f => new Label(
                        text ?? "Test Label",
                        CoordinateFactory.Make(),
                        colour ?? ColourFactory.RandomIdentifier(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
