namespace Compiler.Model
{
    public class Label : AbstractCompilableElement
    {
        public Label(
            string text,
            Coordinate position,
            string colour,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            Text = text;
            Position = position;
            Colour = colour;
        }

        public string Text { get; }
        public Coordinate Position { get; }
        public string Colour { get; }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return string.Format(
                "\"{0}\" {1} {2}",
                this.Text,
                this.Position.ToString(),
                this.Colour
            );
        }
    }
}
