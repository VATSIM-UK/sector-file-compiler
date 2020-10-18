using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class GeoSegment : AbstractCompilableElement, ICompilable
    {
        public GeoSegment(Point firstPoint, Point secondPoint, string colour, string comment)
            : base(comment)
        {
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
            Colour = colour;
        }

        public Point FirstPoint { get; }
        public Point SecondPoint { get; }
        public string Colour { get; }

        public string Compile()
        {
            return String.Format(
                "{0} {1} {2}{3}\r\n",
                this.FirstPoint.Compile(),
                this.SecondPoint.Compile(),
                this.Colour,
                this.CompileComment()
            );
        }
    }
}
