using System;

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

        public override bool Equals(Object obj)
        {
            return obj != null && obj is RadarParameters parameters && Equals(parameters);
        }

        private bool Equals(RadarParameters other)
        {
            return Range == other.Range && Altitude == other.Altitude && ConeSlope == other.ConeSlope;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Range, Altitude, ConeSlope);
        }
    }
}
