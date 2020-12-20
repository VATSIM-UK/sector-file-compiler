using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorActiveFactory
    {
        public static SectorActive Make()
        {
            return GetGenerator().Generate();
        }

        private static Faker<SectorActive> GetGenerator()
        {
            return new Faker<SectorActive>()
                .CustomInstantiator(
                    f => new SectorActive(
                        AirportFactory.GetRandomDesignator(),
                        RunwayFactory.GetRandomDesignator(),
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
