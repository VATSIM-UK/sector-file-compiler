using System;
using System.Collections.Generic;

namespace Compiler.Model
{
    public class SidStar
    {
        public string Type { get; }
        public string Airport { get; }
        public string Runway { get; }
        public string Identifier { get; }
        public List<string> Route { get; }

        public SidStar(
            string type,
            string airport,
            string runway,
            string identifier,
            List<string> route
        )
        {
            this.Type = type;
            this.Airport = airport;
            this.Runway = runway;
            this.Identifier = identifier;
            this.Route = route;
        }
    }
}
