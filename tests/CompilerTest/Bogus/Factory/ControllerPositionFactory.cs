using System.Collections.Generic;
using System.Linq;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class ControllerPositionFactory
    {
        private static readonly string[] Identifiers =
        {
            "LS",
            "LC",
            "LLN",
            "GDA",
            "SST",
            "BJR"
        };
        
        private static readonly string[] Callsigns =
        {
            "LON_S_CTR",
            "EGDD_APP",
            "ESSEX_APP",
            "EGGD_TWR",
            "EGCC_N_TWR",
            "EGGD_GND",
            "EGLL_2_GND",
            "EGBB_"
        };

        private static readonly string[] SquawksRangeStarts =
        {
            "3141",
            "4211",
            "4123",
            "2314",
            "0101"
        };
        
        private static readonly string[] SquawksRangeEnds =
        {
            "5321",
            "6772",
            "5366",
            "7050",
            "6601"
        };
        
        public static string GetIdentifier()
        {
            return new Randomizer().ArrayElement(Identifiers);
        }
        
        public static List<string> GetIdentifierList()
        {
            return new Randomizer().ArrayElements(Identifiers).ToList();
        }

        public static ControllerPosition Make(
            string identifier = null,
            PositionOrder? order = null,
            Definition definition = null
        )
        {
            return new Faker<ControllerPosition>().CustomInstantiator(
                f => new ControllerPosition(
                    f.Random.ArrayElement(Callsigns),
                    "London Control",
                    "123.456",
                    identifier ?? f.Random.ArrayElement(Identifiers),
                    "L",
                    "L",
                    "L",
                    f.Random.ArrayElement(SquawksRangeStarts),
                    f.Random.ArrayElement(SquawksRangeEnds),
                    new List<Coordinate>() {CoordinateFactory.Make()},
                    order ?? PositionOrder.CONTROLLER_POSITION,
                    definition ?? DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            );
        }
    }
}
