using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SidStarRouteFactory
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

        public static SidStarRoute Make(SidStarType type = SidStarType.SID)
        {
            return new Faker<SidStarRoute>()
                .CustomInstantiator(
                    f => new SidStarRoute(
                        type,
                        "SID STAR ROUTE",
                        RouteSegmentFactory.MakeDoublePoint(),
                        new List<RouteSegment>()
                        {
                            RouteSegmentFactory.MakeDoublePoint(),
                            RouteSegmentFactory.MakePointCoordinate(),
                            RouteSegmentFactory.MakeCoordinatePoint()
                        },
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
