namespace Compiler.Model
{
    public class Fix : AbstractCompilableElement
    {
        public string Identifier { get; }
        public Coordinate Coordinate { get; }

        public Fix(
            string identifier,
            Coordinate coordinate,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            Identifier = identifier;
            Coordinate = coordinate;
        }

        public override string GetCompileData()
        {
            return string.Format(
                "{0} {1}",
                this.Identifier,
                this.Coordinate.ToString()
            );
        }
    }
}
