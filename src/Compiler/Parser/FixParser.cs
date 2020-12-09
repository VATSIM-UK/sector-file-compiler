using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Input;

namespace Compiler.Parser
{
    public class FixParser : ISectorDataParser
    {
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public FixParser(
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) {
            this.elements = elements;
            this.eventLogger = eventLogger;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            foreach (SectorData line in data)
            {
                if (line.dataSegments.Count != 3)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Incorrect number of FIX segments", line)
                    );
                    continue;
                }

                // Parse the coordinate
                Coordinate parsedCoordinate = CoordinateParser.Parse(line.dataSegments[1], line.dataSegments[2]);
                if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid coordinate format: " + data.CurrentLine, line)
                    );
                    return;
                }

                this.elements.Add(
                    new Fix(line.dataSegments[0], parsedCoordinate, line.definition, line.docblock, line.inlineComment)
                );
            }
        }
    }
}
