using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorLineDisplayRuleFactory
    {
        public static SectorlineDisplayRule Make()
        {
            return GetGenerator().Generate();
        }

        private static Faker<SectorlineDisplayRule> GetGenerator()
        {
            return new Faker<SectorlineDisplayRule>()
                .CustomInstantiator(
                    f => new SectorlineDisplayRule(
                        f.Random.String2(4),
                        f.Random.String2(4),
                        f.Random.String2(4),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }

        public static List<SectorlineDisplayRule> MakeList(int count = 1)
        {
            return GetGenerator().Generate(count).ToList();
        }
    }
}
