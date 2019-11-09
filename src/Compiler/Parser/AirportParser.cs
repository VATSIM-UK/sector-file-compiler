using System;
using System.Collections.Generic;
using Compiler.Event;
using Compiler.Error;
using Compiler.Model;

namespace Compiler.Parser
{
    public class AirportParser : AbstractSectorElementParser
    {
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public AirportParser(MetadataParser metadataParser, SectorElementCollection elements, IEventLogger eventLogger) : base(metadataParser)
        {
            this.elements = elements;
            this.eventLogger = eventLogger;
        }

        public override void ParseData(SectorFormatData data)
        {
            int linesParsed = 0;
            SectorFormatLine nameLine = new SectorFormatLine("", "");
            SectorFormatLine coordinateLine = new SectorFormatLine("", "");
            SectorFormatLine frequencyLine = new SectorFormatLine("", "");

            // Loop the lines to get the data out
            for(int i = 0; i < data.lines.Count; i++)
            {
                if (this.ParseMetadata(data.lines[i]))
                {
                    continue;
                }

                SectorFormatLine parsedLine = this.ParseLine(data.lines[i]);
                if (linesParsed == 0)
                {
                    nameLine = parsedLine;
                    linesParsed++;
                    continue;
                } else if (linesParsed == 1) {
                    coordinateLine = parsedLine;
                    linesParsed++;
                    continue;
                } else if (linesParsed == 2) {
                    frequencyLine = parsedLine;
                    linesParsed++;
                    continue;
                }

                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid number of airport lines", data.fileName, 0)
                );
                return;
            }

            // Check the syntax
            if (linesParsed != 3)
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid number of airport lines", data.fileName, 0)
                );
                return;
            }

            // Parse the coordinate
            string[] coordinate = coordinateLine.data.Split(' ');
            if (coordinate.Length != 2)
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid coordinate format: " + data.lines[1], data.fileName, 0)
                );
                return;
            }

            Coordinate parsedCoordinate = CoordinateParser.Parse(coordinate[0], coordinate[1]);
            if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid coordinate format: " + data.lines[1], data.fileName, 0)
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
