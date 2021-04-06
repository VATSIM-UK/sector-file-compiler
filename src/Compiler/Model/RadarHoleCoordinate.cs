namespace Compiler.Model
{
    public class RadarHoleCoordinate: AbstractCompilableElement
    {
        public Coordinate Coordinate { get; }

        public RadarHoleCoordinate(
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
