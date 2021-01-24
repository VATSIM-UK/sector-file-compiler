namespace Compiler.Model
{
    public class CentrelineStarter: RunwayCentreline
    {
        public bool IsExtended { get; }

        public CentrelineStarter(
            bool isExtended,
            RunwayCentrelineSegment centrelineSegment,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(centrelineSegment, definition, docblock, inlineComment)
        {
            IsExtended = isExtended;
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            string name = IsExtended ? "Extended centrelines" : "Centrelines";
            return $"{name} {CentrelineSegment}";
        }
    }
}