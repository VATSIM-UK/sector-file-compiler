using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class InfoCoordinate : AbstractCompilableElement
    {
        public InfoCoordinate(
            Coordinate coordinate,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            this.Coordinate = coordinate;
        }

        public Coordinate Coordinate { get; }

        public override string GetCompileData()
        {
            return this.Coordinate.ToString();
        }
    }
}
