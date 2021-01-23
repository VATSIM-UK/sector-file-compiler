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

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"{Colour ?? ""} {Point}".TrimEnd();
        }
    }
}
