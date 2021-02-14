namespace Compiler.Model
{
    public class GroundNetworkCoordinate: AbstractCompilableElement
    {
        public Coordinate Coordinate { get; }

        public GroundNetworkCoordinate(
            Coordinate coordinate,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            Coordinate = coordinate;
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"COORD:{Coordinate.latitude}:{Coordinate.longitude}";
        }
    }
}