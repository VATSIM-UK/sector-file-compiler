using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class Ndb : AbstractCompilableElement
    {
        public string Identifier { get; }
        public string Frequency { get; }
        public Coordinate Coordinate { get; }

        public Ndb(
            string identifier,
            string frequency,
            Coordinate coordinate,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base (definition, docblock, inlineComment)
        {
            this.Identifier = identifier;
            this.Frequency = frequency;
            this.Coordinate = coordinate;
        }

        public override string GetCompileData()
        {
            return string.Format(
                "{0} {1} {2}",
                this.Identifier,
                this.Frequency,
                this.Coordinate.ToString()
            );
        }
    }
}
