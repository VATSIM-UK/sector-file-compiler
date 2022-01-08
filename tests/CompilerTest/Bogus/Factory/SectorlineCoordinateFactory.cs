using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorlineCoordinateFactory
    {
        public static SectorlineCoordinate Make(Coordinate? coordinate = null)
        {
            return GetGenerator(coordinate).Generate();
        }

        public static Faker<SectorlineCoordinate> GetGenerator(Coordinate? coordinate = null)
        {
            return new Faker<SectorlineCoordinate>()
                .CustomInstantiator(
                    _ => new SectorlineCoordinate(
                        coordinate ?? CoordinateFactory.Make(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }

        public static List<SectorlineCoordinate> MakeList(int count = 1)
        {
            return GetGenerator().Generate(count).ToList();
        }
    }
}
