namespace Compiler.Model
{
    public class InfoMilesPerDegreeLongitude : AbstractCompilableElement
    {
        public InfoMilesPerDegreeLongitude(
            double miles,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            this.Miles = miles;
        }

        public double Miles { get; }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return this.Miles.ToString("n2");
        }
    }
}
