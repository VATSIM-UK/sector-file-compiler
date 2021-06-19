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
            Definition definition = null,
            string name = null
        )
        {
            return GetGenerator(colour, points, definition, name).Generate();
        }

        private static Faker<Region> GetGenerator(
            string colour = null,
            List<Point> points = null,
            Definition definition = null,
            string name = null
        )
        {
            return new Faker<Region>()
                .CustomInstantiator(
                    faker => new Region(
                        name ?? faker.Random.Word(),
                        points == null ? RegionPointFactory.MakeList(2, colour) : points.Select(p => RegionPointFactory.Make(colour, p)).ToList(),
                        definition ?? DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }
    }
}
