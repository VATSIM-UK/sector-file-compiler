﻿using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class VorFactory
    {
        private static readonly string[] Identifiers = new[] {
            "BNN",
            "KOK",
            "LAM",
            "BCN",
            "MCT",
            "TRN",
            "TLA",
            "SAM"
        };

        public static Vor Make(string identifier = null)
        {
            return new Faker<Vor>()
                .CustomInstantiator(
                    f => new Vor(
                        identifier ?? f.Random.ArrayElement(Identifiers),
                        "123.456",
                        CoordinateFactory.Make(),
                        DefinitionFactory.Make(),
                        DocblockFactory.Make(),
                        CommentFactory.Make()
                    )
                );
        }
    }
}
