using System;
using System.Collections.Generic;
using Compiler.Event;
using Compiler.Error;
using Compiler.Model;
using Compiler.Validate;
using Compiler.Input;

namespace Compiler.Parser
{
    public class AirportParser : ISectorDataParser
    {
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public AirportParser(
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) {
            this.elements = elements;
            this.eventLogger = eventLogger;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            int linesParsed = 0;

            SectorData nameLine = new SectorData();
            SectorData coordinateLine = new SectorData();
            SectorData frequencyLine = new SectorData();

            // Loop the lines to get the data out
            foreach (SectorData line in data)
            {
                if (linesParsed == 0) {
                    nameLine = line;
                    linesParsed++;
                    continue;
                } else if (linesParsed == 1) {
                    coordinateLine = line;
                    linesParsed++;
                    continue;
                } else if (linesParsed == 2) {
                    frequencyLine = line;
                    linesParsed++;
                    continue;
                }

                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid number of airport lines", line)
                );
                return;
            }

            // Check the syntax
            if (linesParsed != 3)
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid number of airport lines", data.FullPath)
                );
                return;
            }

            // Parse the coordinate
            if (coordinateLine.dataSegments.Count != 2)
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid coordinate format for airport: " + coordinateLine.rawData, coordinateLine)
                );
                return;
            }

            Coordinate parsedCoordinate = CoordinateParser.Parse(coordinateLine.dataSegments[0], coordinateLine.dataSegments[1]);
            if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid coordinate format for airport: " + coordinateLine.rawData, coordinateLine)
                );
                return;
            }

            this.elements.Add(
                new Airport(
                    nameLine.rawData,
                    data.GetParentDirectory(),
                    parsedCoordinate,
                    frequencyLine.rawData,
                    nameLine.definition,
                    nameLine.docblock,
                    nameLine.inlineComment
                )
            );
        }
    }
}
