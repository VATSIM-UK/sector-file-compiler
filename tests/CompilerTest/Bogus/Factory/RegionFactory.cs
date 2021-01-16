using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class RegionFactory
    {
        public static Region Make(
            string colour = null,
            List<Point> points = null,
            Definition definition = null
        )
        {
            return GetGenerator(colour, points, definition).Generate();
        }

        private static Faker<Region> GetGenerator(
            string colour = null,
            List<Point> points = null,
            Definition definition = null
        )
        {
            return new Faker<Region>()
                .CustomInstantiator(
                    _ => new Region(
                        "REGION TEST",
                        points == null ? RegionPointFactory.MakeList(2, colour) : points.Select(p => RegionPointFactory.Make(colour, p)).ToList(),
                        definition ?? DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }
    }
}
