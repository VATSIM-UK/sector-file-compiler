using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class CircleSectorlineFactory
    {
        public static CircleSectorline Make(string name = null, string centre = null, List<SectorlineDisplayRule> displayRules = null, Definition definition = null)
        {
            return GetGenerator(name, centre, displayRules, definition).Generate();
        }

        private static Faker<CircleSectorline> GetGenerator(string name = null, string centre = null, List<SectorlineDisplayRule> displayRules = null, Definition definition = null)
        {
            return new Faker<CircleSectorline>()
                .CustomInstantiator(
                    f => new CircleSectorline(
                        name ?? f.Random.String2(5),
                        centre ?? f.Random.String2(4),
                        f.Random.Double(0.5D, 10D),
                        displayRules ?? SectorLineDisplayRuleFactory.MakeList(2),
                        definition ?? DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }
    }
}
