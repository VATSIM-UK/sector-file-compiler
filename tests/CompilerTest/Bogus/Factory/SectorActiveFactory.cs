using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorActiveFactory
    {
        public static SectorActive Make(string airport = null, string runway = null)
        {
            return GetGenerator(airport, runway).Generate();
        }

        private static Faker<SectorActive> GetGenerator(string airport = null, string runway = null)
        {
            return new Faker<SectorActive>()
                .CustomInstantiator(
                    _ => new SectorActive(
                        airport ?? AirportFactory.GetRandomDesignator(),
                        runway ?? RunwayFactory.GetRandomDesignator(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }
        
        public static List<SectorActive> MakeList(int count = 1)
        {
            return GetGenerator().Generate(count);
        }
    }
}
