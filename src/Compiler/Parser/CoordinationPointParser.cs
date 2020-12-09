using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Input;

namespace Compiler.Parser
{
    public class CoordinationPointParser: ISectorDataParser
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public CoordinationPointParser(
            SectorElementCollection sectorElements,
            IEventLogger errorLog
        ) {
            this.sectorElements = sectorElements;
            this.errorLog = errorLog;
        }

        public void ParseData(AbstractSectorDataFile file)
        {
            foreach (SectorData line in file)
            {
                if (line.dataSegments.Count != 11)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Incorrect number of Coordination Point segments", line)
                    );
                    return;
                }

                if (
                    line.dataSegments[0] != CoordinationPoint.POINT_TYPE_FIR &&
                    line.dataSegments[0] != CoordinationPoint.POINT_TYPE_INTERNAL
                )
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Unknown Coordination point type " + line.dataSegments[0], line)
                    );
                    return;
                }

                if (
                    line.dataSegments[1].Length != 4 &&
                    line.dataSegments[2] != "*"
                )
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Cannot specify a runway without a departure airport " + line.dataSegments[0], line)
                    );
                    return;
                }

                if (
                    line.dataSegments[4].Length != 4 &&
                    line.dataSegments[5] != "*"
                )
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Cannot specify a runway without an arrival airport " + line.dataSegments[0], line)
                    );
                    return;
                }

                if (
                    line.dataSegments[8] != CoordinationPoint.DATA_NOT_SPECIFIED &&
                    line.dataSegments[9] != CoordinationPoint.DATA_NOT_SPECIFIED
                )
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Cannot set both descend and climb level " + line.dataSegments[0], line)
                    );
                    return;
                }

                if (
                    line.dataSegments[8] != CoordinationPoint.DATA_NOT_SPECIFIED &&
                    (!int.TryParse(line.dataSegments[8], out int climbLevel) ||
                    climbLevel < 0)
                )
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid coordination point climb level " + line.dataSegments[0], line)
                    );
                    return;
                }

                if (
                    line.dataSegments[9] != CoordinationPoint.DATA_NOT_SPECIFIED &&
                    (!int.TryParse(line.dataSegments[9], out int descendLevel) ||
                    descendLevel < 0)
                )
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid coordination point descend level " + line.dataSegments[0], line)
                    );
                    return;
                }

                this.sectorElements.Add(
                    new CoordinationPoint(
                        line.dataSegments[0] == CoordinationPoint.POINT_TYPE_FIR,
                        line.dataSegments[1],
                        line.dataSegments[2],
                        line.dataSegments[3],
                        line.dataSegments[4],
                        line.dataSegments[5],
                        line.dataSegments[6],
                        line.dataSegments[7],
                        line.dataSegments[8],
                        line.dataSegments[9],
                        line.dataSegments[10],
                        line.definition,
                        line.docblock,
                        line.inlineComment
                    )
                );
            }
        }
    }
}
