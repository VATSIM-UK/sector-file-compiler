using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorArrivalAirportsFactory
    {
        public static SectorArrivalAirports Make()
        {
            return GetGenerator().Generate();
        }

        private static Faker<SectorArrivalAirports> GetGenerator()
        {
            return new Faker<SectorArrivalAirports>()
                .CustomInstantiator(
                    f => new SectorArrivalAirports(
                        AirportFactory.GetListOfDesignators(),
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
