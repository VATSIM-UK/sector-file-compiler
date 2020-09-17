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
            string output = String.Format(
                "{0} {1}",
                this.Identifier.PadRight(26, ' '),
                this.Segments[0].Compile()
            );

            for (int i = 1; i < this.Segments.Count; i++)
            {
                output += "".PadRight(this.Identifier.PadRight(26, ' ').Length + 1) + this.Segments[i].Compile();
            }

            return output;
        }
    }
}
