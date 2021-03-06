﻿using System.Collections.Generic;
using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class RegionPointFactory
    {
       
        public static RegionPoint Make(string colour = null, Point point = null)
        {
            return GetGenerator(colour, point).Generate();
        }

        public static Faker<RegionPoint> GetGenerator(string colour = null, Point point = null)
        {
            return new Faker<RegionPoint>().CustomInstantiator(
                _ => new RegionPoint(
                    point ?? PointFactory.Make(),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make(),
                    colour ?? "red"
                )
            );
        }
        
        public static List<RegionPoint> MakeList(int count = 1, string colour = null, Point point = null)
        {
            return GetGenerator(colour).Generate(count);
        }
    }
}
