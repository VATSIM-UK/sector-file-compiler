using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class CoordinationPointFactory
    {
        public static CoordinationPoint Make(bool firCopx = false, string coordinationPoint = null, string priorPoint = null, string nextPoint = null, string fromSector = null, string toSector = null)
        {
            return new Faker<CoordinationPoint>()
                .CustomInstantiator(
                    f => new CoordinationPoint(
                        firCopx,
                        FixFactory.RandomIdentifier(),
                        "",
                        coordinationPoint ?? FixFactory.RandomIdentifier(),
                        nextPoint ?? FixFactory.RandomIdentifier(),
                        "",
                        fromSector ?? "AB",
                        toSector ?? "CD",
                        "6000",
                        null,
                        "TEST",
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
        
        public static CoordinationPoint MakeAirport(bool firCopx = false, string departureAirport = null, string departureRunway = null, string arrivalAirport = null, string arrivalRunway = null)
        {
            return new Faker<CoordinationPoint>()
                .CustomInstantiator(
                    f => new CoordinationPoint(
                        firCopx,
                        departureAirport ?? AirportFactory.GetRandomDesignator(),
                        departureRunway ?? RunwayFactory.GetRandomDesignator(),
                        FixFactory.RandomIdentifier(),
                        arrivalAirport ?? AirportFactory.GetRandomDesignator(),
                        arrivalRunway ?? RunwayFactory.GetRandomDesignator(),
                        "AB",
                        "CD",
                        "6000",
                        null,
                        "TEST",
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
