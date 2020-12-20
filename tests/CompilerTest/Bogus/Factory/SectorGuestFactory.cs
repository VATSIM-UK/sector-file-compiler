using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorGuestFactory
    {
        public static SectorGuest Make()
        {
            return GetGenerator().Generate();
        }

        private static Faker<SectorGuest> GetGenerator()
        {
            return new Faker<SectorGuest>()
                .CustomInstantiator(
                    f => new SectorGuest(
                        ControllerPositionFactory.GetIdentifier(),
                        AirportFactory.GetRandomDesignator(),
                        AirportFactory.GetRandomDesignator(),
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
