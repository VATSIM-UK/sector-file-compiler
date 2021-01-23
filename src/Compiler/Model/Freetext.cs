namespace Compiler.Model
{
    public class Freetext : AbstractCompilableElement
    {
        public Freetext(
            string title,
            string text,
            Coordinate coordinate,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            Title = title;
            Text = text;
            Coordinate = coordinate;
        }

        public string Title { get; }
        public string Text { get; }
        public Coordinate Coordinate { get; }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"{this.Coordinate.latitude}:{this.Coordinate.longitude}:{this.Title}:{this.Text}";
        }
    }
}
