using System;
using System.Collections.Generic;
using Compiler.Event;
using Compiler.Error;
using Compiler.Model;
using Compiler.Validate;
using Compiler.Input;

namespace Compiler.Parser
{
    public class AirportParser : AbstractSectorElementParser, ISectorDataParser
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

        public void ParseData(AbstractSectorDataFile data)
        {
            int linesParsed = 0;

            int icaoLineNumber = 0;
            SectorFormatLine icaoLine = new SectorFormatLine("", new List<string>(), "");
            int nameLineNumber = 0;
            SectorFormatLine nameLine = new SectorFormatLine("", new List<string>(), "");
            int coordinateLineNumber = 0;
            SectorFormatLine coordinateLine = new SectorFormatLine("", new List<string>(), "");
            int frequencyLineNumber = 0;
            SectorFormatLine frequencyLine = new SectorFormatLine("", new List<string>(), "");

            // Loop the lines to get the data out
            foreach (string line in data)
            {
                if (this.ParseMetadata(line))
                {
                    continue;
                }

                SectorFormatLine parsedLine = this.sectorLineParser.ParseLine(line);
                if (linesParsed == 0)
                {
                    icaoLine = parsedLine;
                    icaoLineNumber = data.CurrentLineNumber;
                    linesParsed++;
                    continue;
                } else if (linesParsed == 1) {
                    nameLine = parsedLine;
                    nameLineNumber = data.CurrentLineNumber;
                    linesParsed++;
                    continue;
                } else if (linesParsed == 2) {
                    coordinateLine = parsedLine;
                    coordinateLineNumber = data.CurrentLineNumber;
                    linesParsed++;
                    continue;
                } else if (linesParsed == 3) {
                    frequencyLine = parsedLine;
                    frequencyLineNumber = data.CurrentLineNumber;
                    linesParsed++;
                    continue;
                }

                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid number of airport lines", data.FullPath, data.CurrentLineNumber)
                );
                return;
            }

            // Check the syntax
            if (linesParsed != 4)
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid number of airport lines", data.FullPath, data.CurrentLineNumber)
                );
                return;
            }

            // Parse the coordinate
            if (!AirportValidator.IcaoValid(icaoLine.data))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid airport ICAO: " + icaoLine, data.FullPath, icaoLineNumber)
                );
                return;
            }

            // Parse the coordinate
            if (coordinateLine.dataSegments.Count != 2)
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid coordinate format: " + coordinateLine, data.FullPath, coordinateLineNumber)
                );
                return;
            }

            Coordinate parsedCoordinate = CoordinateParser.Parse(coordinateLine.dataSegments[0], coordinateLine.dataSegments[1]);
            if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid coordinate format: " + coordinateLine, data.FullPath, coordinateLineNumber)
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
                    icaoLine.data,
                    parsedCoordinate,
                    frequencyLine.data,
                    String.Join(", ", validComments)
                )
            );
        }
    }
}
