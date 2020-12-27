using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class GeoSegmentFactory
    {
        public static GeoSegment Make()
        {
            return GetGenerator().Generate();
        }

        private static Faker<GeoSegment> GetGenerator()
        {
            return new Faker<GeoSegment>()
                .CustomInstantiator(
                    f => new GeoSegment(
                        PointFactory.Make(),
                        PointFactory.Make(),
                        "red",
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }
        
        public static List<GeoSegment> MakeList(int count = 1)
        {
            return GetGenerator().Generate(count);
        }
    }
}
