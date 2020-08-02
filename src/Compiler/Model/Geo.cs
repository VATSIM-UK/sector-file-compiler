using System.Collections.Generic;
using System.Linq;

namespace Compiler.Model
{
    public class Geo : ICompilable
    {
        public Geo(string name, List<GeoSegment> segments)
        {
            Name = name;
            Segments = segments;
        }
        public string Name { get; }
        public List<GeoSegment> Segments { get; }

        public string Compile()
        {
            return string.Format(
                "{0} {1}",
                this.Name.PadRight(27, ' '),
                this.Segments.Aggregate("", (segmentString, segment) => segmentString + segment.Compile())
            );
        }
    }
}
