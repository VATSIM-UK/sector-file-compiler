using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace CompilerTest.Bogus.Factory
{
    static class AirportFactory
    {
        private static readonly string[] designators = new[] {
            "EGLL",
            "LXGB",
            "EGSS",
            "EGPH",
            "EIDW",
            "EHAM",
            "EGGD",
            "EGJJ"
        };
        
        public static string GetRandomDesignator()
        {
            return new Randomizer().ArrayElement(designators);
        }

        public static List<string> GetListOfDesignators()
        {
            return new Randomizer().ArrayElements(designators).ToList();
        }
    }
}
