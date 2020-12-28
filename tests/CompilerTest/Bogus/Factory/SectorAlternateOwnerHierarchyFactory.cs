using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorAlternateOwnerHierarchyFactory
    {
        public static SectorAlternateOwnerHierarchy Make()
        {
            return GetGenerator().Generate();
        }

        private static Faker<SectorAlternateOwnerHierarchy> GetGenerator()
        {
            return new Faker<SectorAlternateOwnerHierarchy>()
                .CustomInstantiator(
                    f => new SectorAlternateOwnerHierarchy(
                        f.Random.String2(4),
                        ControllerPositionFactory.GetIdentifierList(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }
        
        public static List<SectorAlternateOwnerHierarchy> MakeList(int count)
        {
            return GetGenerator().Generate(count);
        }
    }
}
