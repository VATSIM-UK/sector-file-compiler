using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class RunwayCentrelines : AbstractSectorElement, ICompilable
    {
        public RunwayCentrelines(List<Coordinate> coordinates, string comment)
            : base(comment)
        {
            Coordinates = coordinates;
        }

        public List<Coordinate> Coordinates { get; }

        public string Compile()
        {
            return this.Coordinates.Aggregate(
                "",
                (coordinateString, coordinate) => coordinateString + coordinate.ToString() + "\r\n"
            );
        }
    }
}
