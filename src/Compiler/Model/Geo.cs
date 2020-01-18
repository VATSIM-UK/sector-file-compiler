using System.Collections.Generic;

namespace Compiler.Model
{
    public class Geo : AbstractSectorElement, ICompilable
    {
        public Geo(
            Point startPoint,
            Point endPoint,
            string colour,
            string comment
        ) : base(comment)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Colour = colour;
        }

        public Point StartPoint { get; }
        public Point EndPoint { get; }
        public string Colour { get; }

        public string Compile()
        {
            return string.Format(
                "{0} {1} {2}{3}\r\n",
                this.StartPoint.Compile(),
                this.EndPoint.Compile(),
                this.Colour,
                this.CompileComment()
            );
        }
    }
}
