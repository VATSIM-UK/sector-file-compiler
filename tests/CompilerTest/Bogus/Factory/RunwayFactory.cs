using Bogus;

namespace CompilerTest.Bogus.Factory
{
    static class RunwayFactory
    {
        private static readonly string[] designators = new[] {
            "09L",
            "23R",
            "01",
            "36",
            "09",
            "27",
            "02G",
            "31"
        };
        
        public static string GetRandomDesignator()
        {
            return new Randomizer().ArrayElement(designators);
        }
    }
}
