using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class RouteSegmentFactory
    {
        private static readonly string[] identifiers = new[] {
            "DIKAS",
            "PENIL",
            "UPGAS",
            "MONTY",
            "KONAN",
            "PEDIG",
            "NORBO",
            "ATSIX"
        };

        public static RouteSegment MakeDoublePoint(string identifier1 = null, string identifier2 = null)
        {
            return new Faker<RouteSegment>()
                .CustomInstantiator(
                    f => new RouteSegment(
                        f.Random.String2(4),
                        new Point(identifier1 ?? f.Random.ArrayElement(identifiers)),
                        new Point(identifier2 ?? f.Random.ArrayElement(identifiers)),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
        
        public static RouteSegment MakePointCoordinate(string pointIdentifier = null)
        {
            return new Faker<RouteSegment>()
                .CustomInstantiator(
                    f => new RouteSegment(
                        f.Random.String2(4),
                        new Point(pointIdentifier ?? f.Random.ArrayElement(identifiers)),
                        new Point(CoordinateFactory.Make()),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
        
        public static RouteSegment MakeCoordinatePoint(string pointIdentifier = null)
        {
            return new Faker<RouteSegment>()
                .CustomInstantiator(
                    f => new RouteSegment(
                        f.Random.String2(4),
                        new Point(CoordinateFactory.Make()),
                        new Point(pointIdentifier ?? f.Random.ArrayElement(identifiers)),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
