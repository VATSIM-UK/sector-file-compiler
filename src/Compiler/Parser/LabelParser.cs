using System.Collections.Generic;
using Compiler.Error;
using Compiler.Event;
using Compiler.Model;
using Compiler.Input;

namespace Compiler.Parser
{
    public class LabelParser : ISectorDataParser
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger eventLogger;

        public LabelParser(
            SectorElementCollection sectorElements,
            IEventLogger eventLogger
        ) {
            this.sectorElements = sectorElements;
            this.eventLogger = eventLogger;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            foreach (SectorData line in data)
            {
                if (line.dataSegments.Count != 4)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid number of label definition segments", line)
                    );
                    continue;
                }

                // Parse the position coordinate
                Coordinate parsedCoordinate = CoordinateParser.Parse(line.dataSegments[1], line.dataSegments[2]);
                if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid label coordinate format: " + data.CurrentLine, line)
                    );
                    continue;
                }

                // Add to the sector elements
                sectorElements.Add(
                    new Label(
                        line.dataSegments[0],
                        parsedCoordinate,
                        line.dataSegments[3],
                        line.definition,
                        line.docblock,
                        line.inlineComment
                    )
                );
            }
        }
    }
}
