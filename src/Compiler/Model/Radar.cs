namespace Compiler.Model
{
    public class Radar : AbstractCompilableElement
    {
        public string Name { get; }
        public Coordinate Coordinate { get; }
        public RadarParameters PrimaryRadarParameters { get; }
        public RadarParameters SModeRadarParameters { get; }
        public RadarParameters CModeRadarParameters { get; }

        public Radar(
            string name,
            Coordinate coordinate,
            RadarParameters primaryRadarParameters,
            RadarParameters sModeRadarParameters,
            RadarParameters cModeRadarParameters,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            Name = name;
            Coordinate = coordinate;
            PrimaryRadarParameters = primaryRadarParameters;
            SModeRadarParameters = sModeRadarParameters;
            CModeRadarParameters = cModeRadarParameters;
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return
                $"RADAR2:{Name}:{Coordinate.latitude}:{Coordinate.longitude}:{PrimaryRadarParameters}:{SModeRadarParameters}:{CModeRadarParameters}";
        }
    }
}
