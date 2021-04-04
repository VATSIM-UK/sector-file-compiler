namespace Compiler.Model
{
    public class RadarParameters
    {
        public int? Range { get; }
        public int? Altitude { get; }
        public int? ConeSlope { get; }

        public RadarParameters(
            int range,
            int altitude,
            int coneSlope
        ) {
            Range = range;
            Altitude = altitude;
            ConeSlope = coneSlope;
        }

        public RadarParameters()
        {
            
        }

        public override string ToString()
        {
            return $"{Range}:{Altitude}:{ConeSlope}";
        }

        public override bool Equals(object? obj)
        {
            return obj is RadarParameters &&
                   (obj as RadarParameters).Altitude == Altitude &&
                   (obj as RadarParameters).Range == Range &&
                   (obj as RadarParameters).ConeSlope == ConeSlope;
        }
    }
}