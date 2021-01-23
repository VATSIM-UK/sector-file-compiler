using System.Collections.Generic;

namespace Compiler.Model
{
    public class Info : ICompilableElementProvider
    {
        public Info(
            InfoName name,
            InfoCallsign callsign,
            InfoAirport airport,
            InfoLatitude latitude,
            InfoLongitude longitude,
            InfoMilesPerDegreeLatitude milesPerDegreeLatitude,
            InfoMilesPerDegreeLongitude milesPerDegreeLongitude,
            InfoMagneticVariation magneticVariation,
            InfoScale scale
        ){
            Name = name;
            Callsign = callsign;
            Airport = airport;
            Latitude = latitude;
            Longitude = longitude;
            MilesPerDegreeLatitude = milesPerDegreeLatitude;
            MilesPerDegreeLongitude = milesPerDegreeLongitude;
            MagneticVariation = magneticVariation;
            Scale = scale;
        }

        public InfoName Name { get; }
        public InfoCallsign Callsign { get; }
        public InfoAirport Airport { get; }
        public InfoLatitude Latitude { get; }
        public InfoLongitude Longitude { get; }
        public InfoMilesPerDegreeLatitude MilesPerDegreeLatitude { get; }
        public InfoMilesPerDegreeLongitude MilesPerDegreeLongitude { get; }
        public InfoMagneticVariation MagneticVariation { get; }
        public InfoScale Scale { get; }

        public IEnumerable<ICompilableElement> GetCompilableElements()
        {
            return new List<ICompilableElement>()
            {
                this.Name,
                this.Callsign,
                this.Airport,
                this.Latitude,
                this.Longitude,
                this.MilesPerDegreeLatitude,
                this.MilesPerDegreeLongitude,
                this.MagneticVariation,
                this.Scale
            };
        }
    }
}
