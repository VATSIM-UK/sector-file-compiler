using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorFactory
    {
        public static Sector Make(string name = null)
        {
            return GetGenerator(name).Generate();
        }

        private static Faker<Sector> GetGenerator(string name = null)
        {
            return new Faker<Sector>()
                .CustomInstantiator(
                    f => new Sector(
                        name ?? f.Random.String2(5),
                        0,
                        66000,
                        SectorOwnerHierarchyFactory.Make(),
                        SectorAlternateOwnerHierarchyFactory.MakeList(2),
                        SectorActiveFactory.MakeList(1),
                        SectorGuestFactory.MakeList(2),
                        SectorBorderFactory.MakeList(2),
                        SectorArrivalAirportsFactory.MakeList(),
                        SectorDepartureAirportsFactory.MakeList(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }

        public static List<Sector> MakeList(int count, string name = null)
        {
            return GetGenerator(name).Generate(count).ToList();
        }
    }
}
