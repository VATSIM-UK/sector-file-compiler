using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace CompilerTest.Bogus.Factory
{
    static class ControllerPositionFactory
    {
        private static readonly string[] identifier =
        {
            "LS",
            "LC",
            "LLN",
            "GDA",
            "SST",
            "BJR"
        };
        
        public static string GetIdentifier()
        {
            return new Randomizer().ArrayElement(identifier);
        }
        
        public static List<string> GetIdentifierList()
        {
            return new Randomizer().ArrayElements(identifier).ToList();
        }
    }
}
