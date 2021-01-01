using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SidStarRouteFactory
    {
        public static SidStarRoute Make(
            SidStarType type = SidStarType.SID,
            List<RouteSegment> segments = null,
            Definition definition = null
        ) {
            return new Faker<SidStarRoute>()
                .CustomInstantiator(
                    f => new SidStarRoute(
                        type,
                        "SID STAR ROUTE",
                        RouteSegmentFactory.MakeDoublePoint(),
                        segments ?? new List<RouteSegment>()
                        {
                            RouteSegmentFactory.MakeDoublePoint(),
                            RouteSegmentFactory.MakePointCoordinate(),
                            RouteSegmentFactory.MakeCoordinatePoint()
                        },
                        definition ?? DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
