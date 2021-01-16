using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Input;

namespace Compiler.Parser
{
    public class VrpParser : ISectorDataParser
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public VrpParser(
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
                if (line.dataSegments.Count != 3)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Incorrect number of VRP segments", line)
                    );
                    return;
                }

                Coordinate parsedCoordinate = CoordinateParser.Parse(line.dataSegments[1], line.dataSegments[2]);
                if (parsedCoordinate.Equals(CoordinateParser.InvalidCoordinate))
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid coordinate format: " + data.CurrentLine, line)
                    );
                    return;
                }

                this.sectorElements.Add(
                    new Freetext(
                        $"{data.GetParentDirectoryName()} VRPs",
                        line.dataSegments[0],
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
