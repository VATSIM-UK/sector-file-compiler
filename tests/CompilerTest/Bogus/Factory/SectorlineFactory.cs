using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorlineFactory
    {
        public static Sectorline Make(string name = null, string centre = null, List<SectorlineDisplayRule> displayRules = null, Definition definition = null)
        {
            return GetGenerator(name, centre, displayRules, definition).Generate();
        }

        private static Faker<Sectorline> GetGenerator(string name = null, string centre = null, List<SectorlineDisplayRule> displayRules = null, Definition definition = null)
        {
            return new Faker<Sectorline>()
                .CustomInstantiator(
                    f => new Sectorline(
                        name ?? f.Random.String2(5),
                        displayRules ?? SectorLineDisplayRuleFactory.MakeList(2),
                        SectorlineCoordinateFactory.MakeList(4),
                        definition ?? DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }

        public static List<Sectorline> MakeList(int count = 1, string name = null, string centre = null, List<SectorlineDisplayRule> displayRules = null)
        {
            return GetGenerator(name, centre, displayRules).Generate(count).ToList();
        }
    }
}
