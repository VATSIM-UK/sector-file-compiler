using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorArrivalAirportsFactory
    {
        public static SectorArrivalAirports Make(List<string> airports = null)
        {
            return GetGenerator(airports).Generate();
        }

        private static Faker<SectorArrivalAirports> GetGenerator(List<string> airports = null)
        {
            return new Faker<SectorArrivalAirports>()
                .CustomInstantiator(
                    _ => new SectorArrivalAirports(
                        airports ?? AirportFactory.GetListOfDesignators(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }
        
        public static List<SectorArrivalAirports> MakeList(int count = 1)
        {
            return GetGenerator().Generate(count);
        }
    }
}
