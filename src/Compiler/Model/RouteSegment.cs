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
