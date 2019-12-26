using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class SidStarRoute : AbstractSectorElement, ICompilable
    {
        public SidStarRoute(
            string identifier,
            List<Point> points,
            string comment
        )
            : base(comment)
        {
            Identifier = identifier;
            Points = points;
        }

        public string Identifier { get; }
        public List<Point> Points { get; }

        public string Compile()
        {
            return String.Format(
                "{0}{1}{2}",
                this.Identifier.PadRight(27, ' '),
                this.CompilePointsString(),
                this.CompileComment()
            );
        }

        private string CompilePointsString()
        {
            string pointsString;


            pointsString = String.Format(
                "{0}{1}\r\n",
                this.CompilePointsLine(this.Points[0], this.Points[1])
            );

            for (int i = 1; i < this.Points.Count - 1; i++)
            {
                pointsString += String.Format(
                    "{0}{1}\r\n",
                    "".PadRight(27),
                    this.CompilePointsLine(this.Points[i], this.Points[i + 1])
                );
            }

            return pointsString;

        }

        private string CompilePointsLine(Point start, Point end)
        {
            return start.Compile() + " " + end.Compile();
        }
    }
}
