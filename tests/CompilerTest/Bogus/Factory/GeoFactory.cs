using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class GeoFactory
    {
        public static Geo Make(
            string colour = null,
            List<GeoSegment> additionalSegments = null,
            Point firstPoint = null,
            Point secondPoint = null,
            Definition definition = null
        ) {
            return new Faker<Geo>()
                .CustomInstantiator(
                    f => new Geo(
                        "GEO TEST",
                        firstPoint ?? PointFactory.Make(),
                        secondPoint ?? PointFactory.Make(),
                        colour ?? "red",
                        additionalSegments ?? GeoSegmentFactory.MakeList(2),
                        definition ?? DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
