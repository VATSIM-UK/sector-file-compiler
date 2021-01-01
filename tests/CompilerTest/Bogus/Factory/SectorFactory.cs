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
            List<SectorArrivalAirports> arrivalAirports = null,
            List<SectorDepartureAirports> departureAirports = null,
            List<SectorGuest> guests = null,
            SectorOwnerHierarchy owners = null,
            Definition definition = null
        ) {
            return GetGenerator(name, active, alternate, arrivalAirports, departureAirports, guests, owners, definition).Generate();
        }

        private static Faker<Sector> GetGenerator(
            string name = null,
            List<SectorActive> active = null,
            List<SectorAlternateOwnerHierarchy> alternate = null,
            List<SectorArrivalAirports> arrivalAirports = null,
            List<SectorDepartureAirports> departureAirports = null,
            List<SectorGuest> guests = null,
            SectorOwnerHierarchy owners = null,
            Definition definition = null
        ) {
            return new Faker<Sector>()
                .CustomInstantiator(
                    f => new Sector(
                        name ?? f.Random.String2(5),
                        0,
                        66000,
                        owners ?? SectorOwnerHierarchyFactory.Make(),
                        alternate ?? SectorAlternateOwnerHierarchyFactory.MakeList(2),
                        active ?? SectorActiveFactory.MakeList(),
                        guests ?? SectorGuestFactory.MakeList(2),
                        SectorBorderFactory.MakeList(2),
                        arrivalAirports ?? SectorArrivalAirportsFactory.MakeList(),
                        departureAirports ?? SectorDepartureAirportsFactory.MakeList(),
                        definition ?? DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }
    }
}
