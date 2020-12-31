using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class InfoFactory
    {
        public static Info Make(string airport = null)
        {
            return new(
                new InfoName(
                    "Super Cool Sector",
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                ),
                new InfoCallsign(
                    "LON_CTR",
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                ),
                new InfoAirport(
                    airport ?? "EGLL",
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                ),
                new InfoLatitude(
                    "123",
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                ),
                new InfoLongitude(
                    "456",
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                ),
                new InfoMilesPerDegreeLatitude(
                    60,
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                ),
                new InfoMilesPerDegreeLongitude(
                    40.24,
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                ),
                new InfoMagneticVariation(
                    2.1,
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                ),
                new InfoScale(
                    1,
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            );
        }
    }
}
