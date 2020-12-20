using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class SectorOwnerHierarchyFactory
    {
        public static SectorOwnerHierarchy Make()
        {
            return GetGenerator().Generate();
        }

        private static Faker<SectorOwnerHierarchy> GetGenerator()
        {
            return new Faker<SectorOwnerHierarchy>()
                .CustomInstantiator(
                    f => new SectorOwnerHierarchy(
                        ControllerPositionFactory.GetIdentifierList(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );

        }
    }
}
