using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorlineFactory
    {
        public static Sectorline Make(string name = null, List<SectorlineDisplayRule> displayRules = null,
            Definition definition = null, List<SectorlineCoordinate> coordinates = null)
        {
            return GetGenerator(name, displayRules, definition, coordinates).Generate();
        }

        private static Faker<Sectorline> GetGenerator(string name = null,
            List<SectorlineDisplayRule> displayRules = null, Definition definition = null,
            List<SectorlineCoordinate> coordinates = null)
        {
            return new Faker<Sectorline>()
                .CustomInstantiator(
                    f => new Sectorline(
                        name ?? f.Random.String2(5),
                        displayRules ?? SectorLineDisplayRuleFactory.MakeList(2),
                        coordinates ?? SectorlineCoordinateFactory.MakeList(4),
                        definition ?? DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }

        public static List<Sectorline> MakeList(int count = 1, string name = null,
            List<SectorlineDisplayRule> displayRules = null)
        {
            return GetGenerator(name, displayRules).Generate(count).ToList();
        }
    }
}
