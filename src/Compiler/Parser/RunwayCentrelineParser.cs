using Compiler.Error;
using Compiler.Event;
using Compiler.Input;
using Compiler.Model;

namespace Compiler.Parser
{
    public class RunwayCentrelineParser: ISectorDataParser
    {
        private readonly IEventLogger eventLogger;
        private readonly SectorElementCollection sectorElements;

        public RunwayCentrelineParser(
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
                    eventLogger.AddEvent(new SyntaxError("Invalid number of runway centreline segments", line));
                    return;
                }

                if (!CoordinateParser.TryParse(line.dataSegments[0], line.dataSegments[1], out Coordinate firstCoordinate))
                {
                    eventLogger.AddEvent(new SyntaxError("Invalid first runway centreline coordinate", line));
                    return;
                }
                
                if (!CoordinateParser.TryParse(line.dataSegments[2], line.dataSegments[3], out Coordinate secondCoordinate))
                {
                    eventLogger.AddEvent(new SyntaxError("Invalid second runway centreline coordinate", line));
                    return;
                }

                // Create the segment once and share it to save memory
                RunwayCentrelineSegment segment = new(firstCoordinate, secondCoordinate);
                sectorElements.Add(new RunwayCentreline(segment, line.definition, line.docblock, line.inlineComment));
                sectorElements.Add(new ExtendedRunwayCentreline(segment, line.definition, line.docblock, line.inlineComment));
            }
        }
    }
}