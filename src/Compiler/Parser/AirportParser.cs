using System;
using System.Collections.Generic;
using Compiler.Event;
using Compiler.Error;
using Compiler.Model;
using Compiler.Validate;

namespace Compiler.Parser
{
    public class AirportParser : AbstractSectorElementParser, IFileParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public AirportParser(
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

        public void ParseData(SectorFormatData data)
        {
            int linesParsed = 0;
            SectorFormatLine icaoLine = new SectorFormatLine("", new List<string>(), "");
            SectorFormatLine nameLine = new SectorFormatLine("", new List<string>(), "");
            SectorFormatLine coordinateLine = new SectorFormatLine("", new List<string>(), "");
            SectorFormatLine frequencyLine = new SectorFormatLine("", new List<string>(), "");

            // Loop the lines to get the data out
            for (int i = 0; i < data.lines.Count; i++)
            {
                if (this.ParseMetadata(data.lines[i]))
                {
                    continue;
                }

                SectorFormatLine parsedLine = this.sectorLineParser.ParseLine(data.lines[i]);
                if (linesParsed == 0)
                {
                    icaoLine = parsedLine;
                    linesParsed++;
                    continue;
                } else if (linesParsed == 1) {
                    nameLine = parsedLine;
                    linesParsed++;
                    continue;
                } else if (linesParsed == 2) {
                    coordinateLine = parsedLine;
                    linesParsed++;
                    continue;
                } else if (linesParsed == 3) {
                    frequencyLine = parsedLine;
                    linesParsed++;
                    continue;
                }

                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid number of airport lines", data.fullPath, 0)
                );
                return;
            }

            // Check the syntax
            if (linesParsed != 4)
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid number of airport lines", data.fullPath, 0)
                );
                return;
            }

            // Parse the coordinate
            if (!AirportValidator.IcaoValid(icaoLine.dataSegments[0]))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid airport ICAO: " + data.lines[1], data.fullPath, 0)
                );
                return;
            }

            // Parse the coordinate
            if (coordinateLine.dataSegments.Count != 2)
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid coordinate format: " + data.lines[1], data.fullPath, 0)
                );
                return;
            }

            Coordinate parsedCoordinate = CoordinateParser.Parse(coordinateLine.dataSegments[0], coordinateLine.dataSegments[1]);
            if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid coordinate format: " + data.lines[1], data.fullPath, 0)
                );
                return;
            }

            // Create the element, join all the comments together
            List<string> validComments = new List<string>();
            if (nameLine.comment != null)
            {
                validComments.Add(nameLine.comment);
            }

            if (coordinateLine.comment != null)
            {
                validComments.Add(coordinateLine.comment);
            }

            if (frequencyLine.comment != null)
            {
                validComments.Add(frequencyLine.comment);
            }

            this.elements.Add(
                new Airport(
                    nameLine.data,
                    data.parentDirectory,
                    parsedCoordinate,
                    frequencyLine.data,
                    String.Join(", ", validComments)
                )
            );
        }
    }
}
