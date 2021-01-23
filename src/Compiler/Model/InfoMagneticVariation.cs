namespace Compiler.Model
{
    public class InfoMagneticVariation : AbstractCompilableElement
    {
        public InfoMagneticVariation(
            double variation,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            this.Variation = variation;
        }

        public double Variation { get; }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return this.Variation.ToString("n1");
        }
    }
}
