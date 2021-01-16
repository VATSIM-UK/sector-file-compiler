using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class AirwaySegmentFactory
    {
        private static readonly string[] Identifiers = new[]
        {
            "N864",
            "L9",
            "N862",
            "Q41",
            "M185"
        };
        
        public static AirwaySegment Make(AirwayType type = AirwayType.LOW, string identifier = null)
        {
            return new Faker<AirwaySegment>()
                .CustomInstantiator(
                    f => new AirwaySegment(
                        identifier ?? $"{(type == AirwayType.HIGH ? "U" : "")}{f.Random.ArrayElement(Identifiers)}",
                        type,
                        PointFactory.Make(),
                        PointFactory.Make(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
