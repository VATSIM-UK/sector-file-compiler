using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorAlternateOwnerHierarchyFactory
    {
        public static SectorAlternateOwnerHierarchy Make(List<string> positions = null)
        {
            return GetGenerator(positions).Generate();
        }

        private static Faker<SectorAlternateOwnerHierarchy> GetGenerator(List<string> positions = null)
        {
            return new Faker<SectorAlternateOwnerHierarchy>()
                .CustomInstantiator(
                    f => new SectorAlternateOwnerHierarchy(
                        f.Random.String2(4),
                        positions ?? ControllerPositionFactory.GetIdentifierList(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }
        
        public static List<SectorAlternateOwnerHierarchy> MakeList(int count = 1)
        {
            return GetGenerator().Generate(count);
        }
    }
}
