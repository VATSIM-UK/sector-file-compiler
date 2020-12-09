namespace Compiler.Model
{
    public class InfoLongitude : AbstractCompilableElement
    {
        public InfoLongitude(
            string longitude,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            this.Longitude = longitude;
        }

        public string Longitude { get; }

        public override string GetCompileData()
        {
            return this.Longitude;
        }
    }
}
