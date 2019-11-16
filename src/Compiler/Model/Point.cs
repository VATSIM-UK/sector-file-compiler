using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class Point : ICompilable
    {
        // The different types of point
        public const int TYPE_COORDINATE = 0;
        public const int TYPE_IDENTIFIER = 1;

        public Coordinate coordinate { get; }
        public string Identifier { get; }

        public Point(Coordinate coordinate)
        {
            this.coordinate = coordinate;
        }

        public Point(string Identifier)
        {
            this.Identifier = Identifier;
        }

        public int GetType()
        {
            return this.Identifier == null ? Point.TYPE_COORDINATE : Point.TYPE_IDENTIFIER;
        }

        public string Compile()
        {
            return this.Identifier != null
                ? this.Identifier + " " + this.Identifier
                : this.coordinate.ToString();
        }
    }
}
