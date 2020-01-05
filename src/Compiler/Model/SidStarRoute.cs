using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class SidStarRoute : ICompilable
    {
        public SidStarRoute(
            SidStarType type,
            string identifier,
            List<RouteSegment> segments
        ) {
            Type = type;
            Identifier = identifier;
            Segments = segments;
        }

        public SidStarType Type { get; }
        public string Identifier { get; }
        public List<RouteSegment> Segments { get; }

        public string Compile()
        {
            string output = this.Identifier.PadRight(27, ' ') + this.Segments[0].Compile();

            for (int i = 1; i < this.Segments.Count; i++)
            {
                output += "".PadRight(27) + this.Segments[i].Compile();
            }

            return output;
        }
    }
}
