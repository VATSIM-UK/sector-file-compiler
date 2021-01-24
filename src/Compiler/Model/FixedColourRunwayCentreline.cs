namespace Compiler.Model
{
    public class FixedColourRunwayCentreline: RunwayCentreline
    {
        public FixedColourRunwayCentreline(
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