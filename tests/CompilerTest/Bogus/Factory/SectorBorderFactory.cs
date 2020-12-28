using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorBorderFactory
    {
        public static SectorBorder Make()
        {
            return GetGenerator().Generate();
        }

        private static Faker<SectorBorder> GetGenerator()
        {
            return new Faker<SectorBorder>()
                .CustomInstantiator(
                    f => new SectorBorder(
                        new List<string>() {"abc", "def", "ghi"},
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }

        public static List<SectorBorder> MakeList(int count = 1)
        {
            return GetGenerator().Generate(count).ToList();
        }
    }
}
