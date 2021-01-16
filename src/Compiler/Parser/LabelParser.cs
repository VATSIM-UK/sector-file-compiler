using System.Linq;
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
                // Check everything starts ok
                if (line.rawData.Count(c => c == '"') != 2)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Labels may contain exactly two double quotes", line)
                    );
                    continue;
                }

                if (line.rawData[0] != '"')
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Labels must start with a name encased in quotes", line)
                    );
                    continue;
                }

                // Get the name out and take the name segments off
                int endOfNameIndex = this.GetEndOfNameIndex(line);
                string name = string.Join(' ', line.dataSegments.GetRange(0, endOfNameIndex)).Trim('"');
                line.dataSegments.RemoveRange(0, endOfNameIndex);

                if (line.dataSegments.Count != 3)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid number of LABEL segments, expected 3", line)
                    );
                    continue;
                }

                // Parse the position coordinate
                Coordinate parsedCoordinate = CoordinateParser.Parse(line.dataSegments[0], line.dataSegments[1]);
                if (parsedCoordinate.Equals(CoordinateParser.InvalidCoordinate))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid label coordinate format: " + data.CurrentLine, line)
                    );
                    continue;
                }

                // Add to the sector elements
                sectorElements.Add(
                    new Label(
                        name,
                        parsedCoordinate,
                        line.dataSegments[2],
                        line.definition,
                        line.docblock,
                        line.inlineComment
                    )
                );
            }
        }

        private int GetEndOfNameIndex(SectorData line)
        {
            int count = 0;
            int i;
            for (i = 0; i < line.dataSegments.Count; i++)
            {
                count += line.dataSegments[i].Count(c => c == '"');

                if (count == 2)
                {
                    return i + 1;
                }
            }

            return -1;
        }
    }
}
