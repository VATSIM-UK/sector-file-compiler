using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class Info : AbstractSectorElement, ICompilable
    {
        public Info(
            string name,
            string callsign,
            string airport,
            Coordinate coordinate,
            int milesPerDegreeLatitude,
            int milesPerDegreeLongitude,
            double magneticVariation,
            int scale
        ) : base("")
        {
            Name = name;
            Callsign = callsign;
            Airport = airport;
            Coordinate = coordinate;
            MilesPerDegreeLatitude = milesPerDegreeLatitude;
            MilesPerDegreeLongitude = milesPerDegreeLongitude;
            MagneticVariation = magneticVariation;
            Scale = scale;
        }

        public string Name { get; }
        public string Callsign { get; }
        public string Airport { get; }
        public Coordinate Coordinate { get; }
        public int MilesPerDegreeLatitude { get; }
        public int MilesPerDegreeLongitude { get; }
        public double MagneticVariation { get; }
        public int Scale { get; }

        public string Compile()
        {
            return String.Format(
                "{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}\r\n{5}\r\n{6}\r\n{7}\r\n{8}\r\n",
                this.Name,
                this.Callsign,
                this.Airport,
                this.Coordinate.latitude,
                this.Coordinate.longitude,
                this.MilesPerDegreeLatitude,
                this.MilesPerDegreeLongitude,
                this.MagneticVariation.ToString("1n"),
                this.Scale
            );
        }
    }
}
