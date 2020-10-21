using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Input;

namespace Compiler.Parser
{
    public class AirwayParser : ISectorDataParser
    {
        private readonly AirwayType airwayType;
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public AirwayParser(
            AirwayType airwayType,
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) {
            this.airwayType = airwayType;
            this.elements = elements;
            this.eventLogger = eventLogger;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            foreach (SectorData line in data)
            {
                if (line.dataSegments.Count != 4)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Incorrect number of Airway segments", line)
                    );
                    continue;
                }

                // Parse the airway segment point
                Point startPoint = PointParser.Parse(line.dataSegments[0], line.dataSegments[1]);
                if (startPoint.Equals(PointParser.invalidPoint))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid Airway start point format: " + data.CurrentLine, line)
                    );
                    return;
                }


                // Parse the segment endpoint
                Point endPoint = PointParser.Parse(line.dataSegments[2], line.dataSegments[3]);
                if (endPoint.Equals(PointParser.invalidPoint))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid Airway end point format: " + data.CurrentLine, line)
                    );
                    return;
                }

                // Add it to the collection
                this.elements.Add(
                    new AirwaySegment(
                        data.GetFileName(),  // Each airway is in a file with its name
                        this.airwayType,
                        startPoint,
                        endPoint,
                        line.definition,
                        line.docblock,
                        line.inlineComment
                    )
                );
            }
        }
    }
}
