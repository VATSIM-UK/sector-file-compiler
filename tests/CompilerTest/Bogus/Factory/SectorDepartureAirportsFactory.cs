using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorDepartureAirportsFactory
    {
        public static SectorDepartureAirports Make(List<string> airports = null)
        {
            return GetGenerator(airports).Generate();
        }

        private static Faker<SectorDepartureAirports> GetGenerator(List<string> airports = null)
        {
            return new Faker<SectorDepartureAirports>()
                .CustomInstantiator(
                    _ => new SectorDepartureAirports(
                        airports ?? AirportFactory.GetListOfDesignators(),
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
