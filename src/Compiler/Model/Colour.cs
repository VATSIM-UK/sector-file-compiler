namespace Compiler.Model
{
    public class Colour : AbstractCompilableElement
    {
        public Colour(
            string name,
            int value,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; }
        public int Value { get; }

        public override string GetCompileData()
        {
            return string.Format(
                "#define {0} {1}",
                this.Name,
                this.Value
            );
        }
    }
}
