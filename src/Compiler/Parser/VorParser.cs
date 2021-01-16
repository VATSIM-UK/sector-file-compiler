using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Input;
using System.Linq;

namespace Compiler.Parser
{
    public class VorParser : ISectorDataParser
    {
        private readonly SectorElementCollection elements;
        private readonly IFrequencyParser frequencyParser;
        private readonly IEventLogger eventLogger;

        public VorParser(
            IFrequencyParser frequencyParser,
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) {
            this.elements = elements;
            this.frequencyParser = frequencyParser;
            this.eventLogger = eventLogger;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            foreach (SectorData line in data)
            {
                if (line.dataSegments.Count != 4)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Incorrect number of VOR segments", line)
                    );
                    continue;
                }

                // Check the identifier
                if (
                    line.dataSegments[0].Any(char.IsDigit) ||
                    (line.dataSegments[0].Length != 2 && line.dataSegments[0].Length != 3)
                ) {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid VOR identifier: " + line.dataSegments[1], line)
                    );
                    return;
                }

                // Parse the frequency
                if (this.frequencyParser.ParseFrequency(line.dataSegments[1]) == null)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid VOR frequency: " + line.dataSegments[1], line)
                    );
                    return;
                }

                // Parse the coordinate
                Coordinate parsedCoordinate = CoordinateParser.Parse(line.dataSegments[2], line.dataSegments[3]);
                if (parsedCoordinate.Equals(CoordinateParser.InvalidCoordinate))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid coordinate format: " + data.CurrentLine, line)
                    );
                    return;
                }

                this.elements.Add(
                    new Vor(
                        line.dataSegments[0],
                        line.dataSegments[1],
                        parsedCoordinate,
                        line.definition,
                        line.docblock,
                        line.inlineComment
                    )
                );
            }
        }
    }
}
