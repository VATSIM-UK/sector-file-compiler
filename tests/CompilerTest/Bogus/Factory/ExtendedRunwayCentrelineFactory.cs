using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class ExtendedRunwayCentrelineFactory
    {
        public static ExtendedRunwayCentreline Make()
        {
            return new Faker<ExtendedRunwayCentreline>()
                .CustomInstantiator(
                    _ => new ExtendedRunwayCentreline(
                        RunwayCentrelineSegmentFactory.Make(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
