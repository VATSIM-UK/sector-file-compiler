using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class CircleSectorlineFactory
    {
        public static CircleSectorline Make(string name = null, string centre = null)
        {
            return GetGenerator(name).Generate();
        }

        private static Faker<CircleSectorline> GetGenerator(string name = null, string centre = null)
        {
            return new Faker<CircleSectorline>()
                .CustomInstantiator(
                    f => new CircleSectorline(
                        name ?? f.Random.String2(5),
                        centre ?? f.Random.String2(4),
                        f.Random.Double(0.5D, 10D),
                        SectorLineDisplayRuleFactory.MakeList(2),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }

        public static List<CircleSectorline> MakeList(int count, string name = null)
        {
            return GetGenerator(name).Generate(count).ToList();
        }
    }
}
