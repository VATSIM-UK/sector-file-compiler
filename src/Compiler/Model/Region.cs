using System;
using System.Collections.Generic;

namespace Compiler.Model
{
    public class Region : AbstractCompilableElement, ICompilable
    {
        public Region(string name, string colour, List<Point> points, string comment) : base(comment)
        {
            Name = name;
            Colour = colour;
            Points = points;
        }

        public string Name { get; }
        public string Colour { get; }
        public List<Point> Points { get; }

        public string Compile()
        {
            return String.Format(
                "REGIONNAME {0}\r\n{1} {2}",
                this.Name,
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
                pointString += " " + this.Points[i].Compile() + "\r\n";
            }

            return pointString;
        }
    }
}
