using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorGuestFactory
    {
        public static SectorGuest Make(
            string controllerIdentifier = null,
            string firstAirport = null,
            string secondAirport = null
        ) {
            return GetGenerator(controllerIdentifier, firstAirport, secondAirport).Generate();
        }

        private static Faker<SectorGuest> GetGenerator(
            string controllerIdentifier = null,
            string firstAirport = null,
            string secondAirport = null
        ) {
            return new Faker<SectorGuest>()
                .CustomInstantiator(
                    _ => new SectorGuest(
                        controllerIdentifier ?? ControllerPositionFactory.GetIdentifier(),
                        firstAirport ?? AirportFactory.GetRandomDesignator(),
                        secondAirport ?? AirportFactory.GetRandomDesignator(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }
        
        public static List<SectorGuest> MakeList(int count = 1)
        {
            return GetGenerator().Generate(count);
        }
    }
}
