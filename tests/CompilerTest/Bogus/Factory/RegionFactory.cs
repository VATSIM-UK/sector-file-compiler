using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class RegionFactory
    {
        public static Region Make()
        {
            return GetGenerator().Generate();
        }

        private static Faker<Region> GetGenerator()
        {
            return new Faker<Region>()
                .CustomInstantiator(
                    f => new Region(
                        "REGION TEST",
                        RegionPointFactory.MakeList(2),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }
    }
}
