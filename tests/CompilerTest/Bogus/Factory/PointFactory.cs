using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus
{
    static class PointFactory
    {
        private static readonly string[] Identifiers =
        {
            "BIG",
            "BNN",
            "LOREL",
            "ABBOT",
            "OF"
        };
        
        public static Point Make()
        {
            Faker faker = new();
            return faker.Random.Bool()
                ? new Point(CoordinateFactory.Make())
                : new Point(faker.Random.ArrayElement(Identifiers));
        }
    }
}
