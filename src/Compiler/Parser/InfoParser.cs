using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;

namespace Compiler.Parser
{
    public class InfoParser : AbstractSectorElementParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public InfoParser(
            MetadataParser metadataParser,
            ISectorLineParser sectorLineParser,
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) : base(metadataParser)
        {
            this.sectorLineParser = sectorLineParser;
            this.elements = elements;
            this.eventLogger = eventLogger;
        }

        public override void ParseData(SectorFormatData data)
        {
            string name = "";
            string controllerPosition = "";
            string airport = "";
            string latitude = "";
            string longitude = "";
            int milesPerLatitude = 0;
            double milesPerLongitude = 0.0;
            int effectiveLine = 0;
            int scale = 0;
            double magVar = 0.0;
            for (int i = 0; i < data.lines.Count; i++)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(data.lines[i]))
                {
                    continue;
                }

                SectorFormatLine sectorData = this.sectorLineParser.ParseLine(data.lines[i]);

                // First line is the name
                if (effectiveLine == 0)
                {
                    name = sectorData.data;
                    effectiveLine++;
                    continue;
                }

                // Second line is a controller position
                if (effectiveLine == 1)
                {
                    controllerPosition = sectorData.data;
                    effectiveLine++;
                    continue;
                }

                // Third line is the key airport
                if (effectiveLine == 2)
                {
                    airport = sectorData.data;
                    effectiveLine++;
                    continue;
                }

                // Fourth line is the latitude
                if (effectiveLine == 3)
                {
                    latitude = sectorData.data;
                    effectiveLine++;
                    continue;
                }

                // Fifth line is the longitude
                if (effectiveLine == 4)
                {
                    longitude = sectorData.data;

                    // Check the coordinate at this point
                    if (CoordinateParser.Parse(latitude, longitude).Equals(CoordinateParser.invalidCoordinate))
                    {
                        this.eventLogger.AddEvent(
                            new SyntaxError("Invalid INFO coordinate: " + data.lines[i], data.fullPath, i)
                        );
                        return;
                    }

                    effectiveLine++;
                    continue;
                }

                // Sixth line is miles per degree latitude
                if (effectiveLine == 5)
                {
                    if (!int.TryParse(sectorData.data, out milesPerLatitude)) {
                        this.eventLogger.AddEvent(
                            new SyntaxError("Invalid INFO miles per degree latitude: " + data.lines[i], data.fullPath, i)
                        );
                        return;
                    }

                    effectiveLine++;
                    continue;
                }

                // Seventh line is miles per degree longitude
                if (effectiveLine == 6)
                {
                    if (!double.TryParse(sectorData.data, out milesPerLongitude))
                    {
                        this.eventLogger.AddEvent(
                            new SyntaxError("Invalid INFO miles per degree longitude: " + data.lines[i], data.fullPath, i)
                        );
                        return;
                    }

                    effectiveLine++;
                    continue;
                }

                // Eigth line is magnetic variation
                if (effectiveLine == 7)
                {
                    if (!double.TryParse(sectorData.data, out magVar))
                    {
                        this.eventLogger.AddEvent(
                            new SyntaxError("Invalid INFO magvar: " + data.lines[i], data.fullPath, i)
                        );
                        return;
                    }

                    effectiveLine++;
                    continue;
                }

                // Ninth line is scale
                if (effectiveLine == 8)
                {
                    if (!int.TryParse(sectorData.data, out scale))
                    {
                        this.eventLogger.AddEvent(
                            new SyntaxError("Invalid INFO scale: " + data.lines[i], data.fullPath, i)
                        );
                        return;
                    }

                    effectiveLine++;
                    continue;
                }

                // Skip all extra line
            }

            if (effectiveLine != 9)
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Missing INFO data", data.fullPath, 0)
                );
                return;
            }

            this.elements.Add(
                new Info(
                    name,
                    controllerPosition,
                    airport,
                    new Coordinate(latitude, longitude),
                    milesPerLatitude,
                    milesPerLongitude,
                    magVar,
                    scale
                )
            );
        }
    }
}
