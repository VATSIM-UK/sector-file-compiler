using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class GeoFactory
    {
        public static Geo Make()
        {
            return GetGenerator().Generate();
        }

        private static Faker<Geo> GetGenerator()
        {
            return new Faker<Geo>()
                .CustomInstantiator(
                    f => new Geo(
                        "GEO TEST",
                        PointFactory.Make(),
                        PointFactory.Make(),
                        "red",
                        GeoSegmentFactory.MakeList(2),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }
    }
}
