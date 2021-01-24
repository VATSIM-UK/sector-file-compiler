using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class RunwayCentrelineFactory
    {
        public static RunwayCentreline Make()
        {
            return new Faker<RunwayCentreline>()
                .CustomInstantiator(
                    _ => new RunwayCentreline(
                        RunwayCentrelineSegmentFactory.Make(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
