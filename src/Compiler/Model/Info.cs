﻿using System;
using System.Collections.Generic;
using System.Text;

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
                this.MilesPerDegreeLongitude.ToString("n2"),
                this.MagneticVariation.ToString("n1"),
                this.Scale
            );
        }

        public IEnumerable<ICompilableElement> GetCompilableElements()
        {
            return new List<ICompilableElement>()
            {
                this.Name,
                this.Callsign,
                this.Airport,
                this.Coordinate,
                this.MilesPerDegreeLatitude,
                this.MilesPerDegreeLongitude,
                this.MagneticVariation,
                this.Scale
            };
        }
    }
}
