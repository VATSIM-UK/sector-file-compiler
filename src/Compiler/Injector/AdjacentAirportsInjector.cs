using Compiler.Model;

namespace Compiler.Injector
{
    public class AdjacentAirportsInjector
    {
        public static void InjectAdjacentAirportsData(SectorElementCollection collection)
        {
            // Add airport
            collection.Add(
                new Airport(
                    "Show adjacent departure airports",
                    "000A",
                    new Coordinate("S999.00.00.000", "E999.00.00.000"),
                    "199.998",
                    new Definition("Defined by compiler", 0),
                    new Docblock(),
                    new Comment("")
                )
            );
            
            // Add runway
            collection.Add(
                new Runway(
                    "000A",
                    "00",
                    0,
                    new Coordinate("S999.00.00.000", "E999.00.00.000"),
                    "01",
                    0,
                    new Coordinate("S999.00.00.000", "E999.00.00.000"),
                    new Definition("Defined by compiler", 0),
                    new Docblock(),
                    new Comment("")
                )
            );
        }
    }
}
