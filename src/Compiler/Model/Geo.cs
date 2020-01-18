using System.Collections.Generic;

namespace Compiler.Model
{
    public class Geo : AbstractSectorElement, ICompilable
    {
        public Geo(
            Coordinate startPoint,
            Coordinate endPoint,
            string colour,
            string comment
        ) : base(comment)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Colour = colour;
        }

        public Coordinate StartPoint { get; }
        public Coordinate EndPoint { get; }
        public string Colour { get; }

        public string Compile()
        {
            return string.Format(
                "{0} {1} {2}{3}\r\n",
                this.StartPoint.ToString(),
                this.EndPoint.ToString(),
                this.Colour,
                this.CompileComment()
            );
        }
    }
}
