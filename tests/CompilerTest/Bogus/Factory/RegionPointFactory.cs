using System.Collections.Generic;
using Bogus;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Bogus
{
    static class RegionPointFactory
    {
       
        public static RegionPoint Make()
        {
            return GetGenerator().Generate();
        }

        public static Faker<RegionPoint> GetGenerator()
        {
            return new Faker<RegionPoint>().CustomInstantiator(
                f => new RegionPoint(
                    PointFactory.Make(),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make(),
                    "red"
                )
            );
        }
        
        public static List<RegionPoint> MakeList(int count = 1)
        {
            return GetGenerator().Generate(count);
        }
    }
}
