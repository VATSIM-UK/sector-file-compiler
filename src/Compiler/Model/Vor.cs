using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class Vor : AbstractSectorElement, ICompilable
    {
        public string Identifier { get; }
        public string Frequency { get; }
        public Coordinate Coordinate { get; }

        public Vor(string identifier, string frequency, Coordinate coordinate, string comment) : base(comment)
        {
            this.Identifier = identifier;
            this.Frequency = frequency;
            this.Coordinate = coordinate;
        }

        public string Compile()
        {
            return string.Format(
                "{0} {1} {2}{3}\r\n",
                this.Identifier,
                this.Frequency,
                this.Coordinate.ToString(),
                this.CompileComment()
            );
        }
    }
}
