using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class RouteSegment : AbstractCompilableElement, ICompilable
    {
        public RouteSegment(
            Point start,
            Point end,
            string colour = null,
            string comment = null
        )
            : base(comment)
        {
            Start = start;
            End = end;
            Colour = colour;
        }

        public Point Start { get; }
        public Point End { get; }
        public string Colour { get; }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            RouteSegment segment = (RouteSegment)obj;
            return this.Start.Equals(segment.Start) &&
                this.End.Equals(segment.End) &&
                (
                    (this.Colour == null && segment.Colour == null) || 
                    (this.Colour != null && segment.Colour != null && this.Colour.Equals(segment.Colour))
                );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string Compile()
        {
            return String.Format(
                "{0} {1}{2}{3}\r\n",
                this.Start.Compile(),
                this.End.Compile(),
                this.Colour == null ? "" : " " + this.Colour,
                this.CompileComment()
            );
        }
    }
}
