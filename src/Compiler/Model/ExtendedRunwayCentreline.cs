namespace Compiler.Model
{
    public class ExtendedRunwayCentreline: RunwayCentreline
    {
        public ExtendedRunwayCentreline(
            RunwayCentrelineSegment centrelineSegment,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) 
            : base(centrelineSegment, definition, docblock, inlineComment)
        {
        }
        
        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"{base.GetCompileData(elements)} centrelinecolour";
        }
    }
}