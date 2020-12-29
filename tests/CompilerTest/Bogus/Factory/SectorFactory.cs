using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorFactory
    {
        public static Sector Make(
            string name = null,
            List<SectorActive> active = null,
            List<SectorAlternateOwnerHierarchy> alternate = null,
            List<SectorArrivalAirports> arrivalAirports = null
        ) {
            return GetGenerator(name, active, alternate, arrivalAirports).Generate();
        }

        private static Faker<Sector> GetGenerator(
            string name = null,
            List<SectorActive> active = null,
            List<SectorAlternateOwnerHierarchy> alternate = null,
            List<SectorArrivalAirports> arrivalAirports = null
        ) {
            return new Faker<Sector>()
                .CustomInstantiator(
                    f => new Sector(
                        name ?? f.Random.String2(5),
                        0,
                        66000,
                        SectorOwnerHierarchyFactory.Make(),
                        alternate ?? SectorAlternateOwnerHierarchyFactory.MakeList(2),
                        active ?? SectorActiveFactory.MakeList(1),
                        SectorGuestFactory.MakeList(2),
                        SectorBorderFactory.MakeList(2),
                        arrivalAirports ?? SectorArrivalAirportsFactory.MakeList(),
                        SectorDepartureAirportsFactory.MakeList(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }

        public static List<Sector> MakeList(int count, string name = null, List<SectorActive> active = null)
        {
            return GetGenerator(name, active).Generate(count).ToList();
        }
    }
}
