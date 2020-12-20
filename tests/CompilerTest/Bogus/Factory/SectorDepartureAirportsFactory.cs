using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorDepartureAirportsFactory
    {
        public static SectorDepartureAirports Make()
        {
            return GetGenerator().Generate();
        }

        private static Faker<SectorDepartureAirports> GetGenerator()
        {
            return new Faker<SectorDepartureAirports>()
                .CustomInstantiator(
                    f => new SectorDepartureAirports(
                        AirportFactory.GetListOfDesignators(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }
        
        public static List<SectorDepartureAirports> MakeList(int count = 1)
        {
            return GetGenerator().Generate(count);
        }
    }
}
