using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class RouteSegment : AbstractSectorElement, ICompilable
    {
        public RouteSegment(
            Point start,
            Point end,
            string comment
        )
            : base(comment)
        {
            Start = start;
            End = end;
        }

        public Point Start { get; }
        public Point End { get; }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            RouteSegment segment = (RouteSegment)obj;
            return this.Start.Equals(segment.Start) && this.End.Equals(segment.End);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string Compile()
        {
            return String.Format(
                "{0} {1}{2}\r\n",
                this.Start.Compile(),
                this.End.Compile(),
                this.CompileComment()
            );
        }
    }
}
