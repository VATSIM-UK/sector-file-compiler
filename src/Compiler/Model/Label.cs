namespace Compiler.Model
{
    public class Label : AbstractSectorElement, ICompilable
    {
        public Label(string text, Coordinate position, string colour, string comment) : base(comment)
        {
            Text = text;
            Position = position;
            Colour = colour;
        }

        public string Text { get; }
        public Coordinate Position { get; }
        public string Colour { get; }

        public string Compile()
        {
            return string.Format(
                "\"{0}\" {1} {2}{3}\r\n",
                this.Text,
                this.Position.ToString(),
                this.Colour,
                this.CompileComment()
            );
        }
    }
}
