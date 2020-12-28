using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class AirwaySegmentFactory
    {
        private static readonly string[] identifiers = new[]
        {
            "N864",
            "L9",
            "N862",
            "Q41",
            "M185"
        };
        
        public static AirwaySegment Make(AirwayType type = AirwayType.LOW)
        {
            return new Faker<AirwaySegment>()
                .CustomInstantiator(
                    f => new AirwaySegment(
                        $"{(type == AirwayType.HIGH ? "U" : "")}{f.Random.ArrayElement(identifiers)}",
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
