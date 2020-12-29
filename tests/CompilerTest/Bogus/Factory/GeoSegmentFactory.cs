using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class GeoSegmentFactory
    {
        public static GeoSegment Make(string colour = null, Point firstPoint = null, Point secondPoint = null)
        {
            return GetGenerator(colour, firstPoint, secondPoint).Generate();
        }

        private static Faker<GeoSegment> GetGenerator(string colour = null, Point firstPoint = null, Point secondPoint = null)
        {
            return new Faker<GeoSegment>()
                .CustomInstantiator(
                    f => new GeoSegment(
                        firstPoint ?? PointFactory.Make(),
                        secondPoint ?? PointFactory.Make(),
                        colour ?? "red",
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }
        
        public static List<GeoSegment> MakeList(int count = 1, string colour = null, Point firstPoint = null, Point secondPoint = null)
        {
            return GetGenerator(colour, firstPoint, secondPoint).Generate(count);
        }
    }
}
