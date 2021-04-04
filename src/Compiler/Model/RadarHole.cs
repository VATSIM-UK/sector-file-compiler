namespace Compiler.Model
{
    public class RadarHole: AbstractCompilableElement
    {
        public int? PrimaryTop { get; }
        public int? SModeTop { get; }
        public int? CModeTop { get; }

        public RadarHole(
            int? primaryTop,
            int? sModeTop,
            int? cModeTop,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            PrimaryTop = primaryTop;
            SModeTop = sModeTop;
            CModeTop = cModeTop;
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"HOLE:{PrimaryTop}:{SModeTop}:{CModeTop}";
        }
    }
}