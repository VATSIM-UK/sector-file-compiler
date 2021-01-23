namespace Compiler.Model
{
    public class InfoLatitude : AbstractCompilableElement
    {
        public InfoLatitude(
            string latitude,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            this.Latitude = latitude;
        }

        public string Latitude { get; }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return this.Latitude;
        }
    }
}
