using Compiler.Model;

namespace Compiler.Injector
{
    public static class RunwayCentrelineInjector
    {
        public static void InjectRunwayCentrelineData(SectorElementCollection collection)
        {
            // Add normal one
            collection.RunwayCentrelines.Add(
                new CentrelineStarter(
                    true,
                    GetStarterSegment(),
                    new Definition("Defined by compiler", 0),
                    new Docblock(),
                    new Comment("Defined by compiler")
                )
            );
            
            // Add fixed colour
            collection.FixedColourRunwayCentrelines.Add(
                new CentrelineStarter(
                    false,
                    GetStarterSegment(),
                    new Definition("Defined by compiler", 0),
                    new Docblock(),
                    new Comment("Defined by compiler")
                )
            );
        }

        private static RunwayCentrelineSegment GetStarterSegment()
        {
            return new(
                new Coordinate("N000.00.00.000", "E000.00.00.000"),
                new Coordinate("N000.00.00.000", "E000.00.00.000")
            );
        }
    }
}