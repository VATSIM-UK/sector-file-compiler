using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class ArtccSegmentFactory
    {
        public static ArtccSegment Make(ArtccType type = ArtccType.REGULAR, string identifier = null)
        {
            return new Faker<ArtccSegment>()
                .CustomInstantiator(
                    _ => new ArtccSegment(
                        identifier ?? "EGTT",
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
