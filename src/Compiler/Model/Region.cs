using System;
using System.Collections.Generic;

namespace Compiler.Model
{
    public class Region : AbstractSectorElement, ICompilable
    {
        public Region(string colour, List<Point> points, string comment) : base(comment)
        {
            Colour = colour;
            Points = points;
        }

        public string Colour { get; }
        public List<Point> Points { get; }

        public string Compile()
        {
            return String.Format(
                "{0} {1}",
                this.Colour,
                this.CompilePointString()
            );
        }

        private string CompilePointString()
        {
            string pointString = "";
            if (this.Points.Count == 0)
            {
                return pointString;
            }

            pointString += this.Points[0].Compile() + this.CompileComment() + "\r\n";

            for (int i = 1; i < this.Points.Count; i++) 
            {
                pointString += this.Points[i].Compile() + "\r\n";
            }

            return pointString;
        }
    }
}
