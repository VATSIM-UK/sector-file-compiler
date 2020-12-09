using System;

namespace Compiler.Model
{
    public class RegionPoint : AbstractCompilableElement
    {
        public RegionPoint(
            Point point,
            Definition definition,
            Docblock docblock,
            Comment inlineComment,
            string colour = null
        )
            : base(definition, docblock, inlineComment)
        {
            Point = point;
            Colour = colour;
        }

        public Point Point { get; }

        public string Colour { get; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string GetCompileData()
        {
            return String.Format(
                "{0}{1}",
                this.Colour == null ? " " : this.Colour + " ",
                this.Point.ToString()
            );
        }
    }
}
