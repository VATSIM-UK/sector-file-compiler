namespace Compiler.Model
{
    public class RunwayCentreline: AbstractCompilableElement
    {
        public RunwayCentrelineSegment CentrelineSegment { get; }

        public RunwayCentreline(
            RunwayCentrelineSegment centrelineSegment,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            CentrelineSegment = centrelineSegment;
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"{CentrelineSegment}";
        }
    }
}