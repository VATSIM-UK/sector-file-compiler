using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorLineDisplayRuleFactory
    {
        public static SectorlineDisplayRule Make(string controlledSector = null, string firstCompare = null, string secondCompare = null)
        {
            return GetGenerator(controlledSector, firstCompare, secondCompare).Generate();
        }

        private static Faker<SectorlineDisplayRule> GetGenerator(string controlledSector = null, string firstCompare = null, string secondCompare = null)
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

        public static List<SectorlineDisplayRule> MakeList(int count = 1, string controlledSector = null, string firstCompare = null, string secondCompare = null)
        {
            return GetGenerator(controlledSector, firstCompare, secondCompare).Generate(count).ToList();
        }
    }
}
