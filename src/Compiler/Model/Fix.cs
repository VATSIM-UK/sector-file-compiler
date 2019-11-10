using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class Fix : AbstractSectorElement, ICompilable
    {
        public string Identifier { get; }
        public Coordinate Coordinate { get; }

        public Fix(string identifier, Coordinate coordinate, string comment) : base(comment)
        {
            Identifier = identifier;
            Coordinate = coordinate;
        }

        public string Compile()
        {
            return String.Format(
                "{0} {1}{2}\r\n",
                this.Identifier,
                this.Coordinate.ToString(),
                this.Comment
            );
        }
    }
}
