using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class NdbFactory
    {
        private static readonly string[] identifiers = new[] {
            "OX",
            "BRI",
            "CDF",
            "LCY",
            "OF",
            "JY",
        };

        public static Ndb Make(string identifier = null)
        {
            return new Faker<Ndb>()
                .CustomInstantiator(
                    f => new Ndb(
                        identifier ?? f.Random.ArrayElement(identifiers),
                        "123.456",
                        CoordinateFactory.Make(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
