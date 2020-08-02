using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class GeoSegment : AbstractSectorElement, ICompilable
    {
        public GeoSegment(Coordinate firstCoordinate, Coordinate secondCoordinate, string colour, string comment)
            : base(comment)
        {
            FirstCoordinate = firstCoordinate;
            SecondCoordinate = secondCoordinate;
            Colour = colour;
        }

        public Coordinate FirstCoordinate { get; }
        public Coordinate SecondCoordinate { get; }
        public string Colour { get; }

        public string Compile()
        {
            return String.Format(
                "{0} {1} {2}{3}\r\n",
                this.FirstCoordinate.ToString(),
                this.SecondCoordinate.ToString(),
                this.Colour,
                this.CompileComment()
            );
        }
    }
}
