using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Input;

namespace Compiler.Parser
{
    public class FreetextParser : ISectorDataParser
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public FreetextParser(
            SectorElementCollection sectorElements,
            IEventLogger errorLog
        ) {
            this.sectorElements = sectorElements;
            this.errorLog = errorLog;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            foreach (SectorData line in data)
            {
                if (line.dataSegments.Count != 4)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Incorrect number of Freetext segments", line)
                    );
                    return;
                }

                Coordinate parsedCoordinate = CoordinateParser.Parse(line.dataSegments[0], line.dataSegments[1]);
                if (parsedCoordinate.Equals(CoordinateParser.InvalidCoordinate))
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid coordinate format: " + data.CurrentLine, line)
                    );
                    return;
                }


                this.sectorElements.Add(
                    new Freetext(
                        line.dataSegments[2],
                        line.dataSegments[3],
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
