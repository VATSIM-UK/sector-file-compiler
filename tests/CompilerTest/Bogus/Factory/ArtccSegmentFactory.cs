using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class ArtccSegmentFactory
    {
        public static ArtccSegment Make(ArtccType type = ArtccType.REGULAR)
        {
            return new Faker<ArtccSegment>()
                .CustomInstantiator(
                    f => new ArtccSegment(
                        "EGTT",
                        type,
                        PointFactory.Make(),
                        PointFactory.Make(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
