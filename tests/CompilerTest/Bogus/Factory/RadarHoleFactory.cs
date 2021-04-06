using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class RadarHoleFactory
    {
        public static RadarHole Make()
        {
            return new Faker<RadarHole>()
                .CustomInstantiator(
                    f => new RadarHole(
                        f.Random.Int(0, 3000),
                        f.Random.Int(0, 3000),
                        f.Random.Int(0, 3000),
                        new List<RadarHoleCoordinate>
                        {
                            RadarHoleCoordinateFactory.Make(),
                            RadarHoleCoordinateFactory.Make(),
                            RadarHoleCoordinateFactory.Make()
                        },
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
