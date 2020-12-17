namespace Compiler.Model
{
    public class InfoScale : AbstractCompilableElement
    {
        public InfoScale(
            int scale,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            this.Scale = scale;
        }

        public int Scale { get; }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return this.Scale.ToString();
        }
    }
}
