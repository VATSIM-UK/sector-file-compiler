using System.Collections.Generic;
using Compiler.Input;

namespace CompilerTest.Bogus.Factory
{
    static class SectorDataFactory
    {
        public static SectorData Make()
        {
            return new SectorData(
                DocblockFactory.Make(),
                CommentFactory.Make(),
                new List<string> { "abc", "def", "ghi" },
                "abc def ghi",
                DefinitionFactory.Make()
            );
        }
    }
}
