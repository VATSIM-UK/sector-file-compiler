using System;
using System.Collections.Generic;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Input;
using Compiler.Validate;

namespace Compiler.Parser
{
    public class InfoParser : ISectorDataParser
    {
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public InfoParser(
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) {
            this.elements = elements;
            this.eventLogger = eventLogger;
        }

        private InfoName GetName(SectorData line)
        {
            return new(line.rawData, line.definition, line.docblock, line.inlineComment);
        }

        private InfoCallsign GetCallsign(SectorData line)
        {
            if (!CallsignValidator.Validate(line.rawData))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid INFO callsign", line)
                );
                throw new ArgumentNullException();
            }

            return new InfoCallsign(line.rawData, line.definition, line.docblock, line.inlineComment);
        }

        private InfoAirport GetAirport(SectorData line)
        {
            if (!AirportValidator.IcaoValid(line.rawData))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid INFO airport", line)
                );
                throw new ArgumentNullException();
            }

            return new InfoAirport(line.rawData, line.definition, line.docblock, line.inlineComment);
        }

        private InfoLatitude GetLatitude(SectorData latitude, SectorData longitude)
        {
            if (CoordinateParser.Parse(latitude.rawData, longitude.rawData).Equals(CoordinateParser.InvalidCoordinate))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid INFO coordinate", latitude.rawData)
                );
                throw new ArgumentNullException();
            }

            return new InfoLatitude(latitude.rawData, latitude.definition, latitude.docblock, latitude.inlineComment);
        }

        private InfoLongitude GetLongitude(SectorData latitude, SectorData longitude)
        {
            if (CoordinateParser.Parse(latitude.rawData, longitude.rawData).Equals(CoordinateParser.InvalidCoordinate))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid INFO coordinate", latitude.rawData)
                );
                throw new ArgumentNullException();
            }

            return new InfoLongitude(longitude.rawData, longitude.definition, longitude.docblock, longitude.inlineComment);
        }

        private InfoMilesPerDegreeLatitude GetMilesPerDegreeLatitude(SectorData line)
        {
            if (!int.TryParse(line.rawData, out int miles))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid INFO miles per degree latitude", line)
                );
                throw new ArgumentException();
            }

            return new InfoMilesPerDegreeLatitude(miles, line.definition, line.docblock, line.inlineComment);
        }

        private InfoMilesPerDegreeLongitude GetInfoMilesPerDegreeLongitude(SectorData line)
        {
            if (!double.TryParse(line.rawData, out double miles))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid INFO miles per degree longitude", line)
                );
                throw new ArgumentException();
            }

            return new InfoMilesPerDegreeLongitude(miles, line.definition, line.docblock, line.inlineComment);
        }

        private InfoMagneticVariation GetMagneticVariation(SectorData line)
        {
            if (!double.TryParse(line.rawData, out double variation))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid INFO variation", line)
                );
                throw new ArgumentException();
            }

            return new InfoMagneticVariation(variation, line.definition, line.docblock, line.inlineComment);
        }

        private InfoScale GetScale(SectorData line)
        {
            if (!int.TryParse(line.rawData, out int scale))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid INFO scale", line)
                );
                throw new ArgumentException();
            }

            return new InfoScale(scale, line.definition, line.docblock, line.inlineComment);
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            // Get all the lines out up-front
            List<SectorData> lines = new List<SectorData>();
            foreach (SectorData line in data)
            {
                lines.Add(line);
            }

            if (lines.Count != 9)
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid number of INFO lines", data.FullPath)
                );
                return;
            }

            try
            {
                this.elements.Add(
                    new Info(
                        this.GetName(lines[0]),
                        this.GetCallsign(lines[1]),
                        this.GetAirport(lines[2]),
                        this.GetLatitude(lines[3], lines[4]),
                        this.GetLongitude(lines[3], lines[4]),
                        this.GetMilesPerDegreeLatitude(lines[5]),
                        this.GetInfoMilesPerDegreeLongitude(lines[6]),
                        this.GetMagneticVariation(lines[7]),
                        this.GetScale(lines[8])
                    )
                );
            } catch
            {
                // Exceptions dealt with in handler functions
            }
        }
    }
}
