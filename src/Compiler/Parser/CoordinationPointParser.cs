using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;

namespace Compiler.Parser
{
    public class CoordinationPointParser: AbstractSectorElementParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public CoordinationPointParser(
            MetadataParser metadataParser,
            ISectorLineParser sectorLineParser,
            SectorElementCollection sectorElements,
            IEventLogger errorLog
        ) : base(metadataParser)
        {
            this.sectorLineParser = sectorLineParser;
            this.sectorElements = sectorElements;
            this.errorLog = errorLog;
        }

        public override void ParseData(SectorFormatData data)
        {
            for (int i = 0; i < data.lines.Count; i++)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(data.lines[i]))
                {
                    continue;
                }

                SectorFormatLine sectorData = this.sectorLineParser.ParseLine(data.lines[i]);
                if (sectorData.dataSegments.Count != 11)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Incorrect number of Coordination Point segments", data.fullPath, i + 1)
                    );
                    continue;
                }

                if (
                    sectorData.dataSegments[0] != CoordinationPoint.POINT_TYPE_FIR &&
                    sectorData.dataSegments[0] != CoordinationPoint.POINT_TYPE_INTERNAL
                ) {
                    this.errorLog.AddEvent(
                        new SyntaxError("Unknown Coordination point type " + sectorData.dataSegments[0], data.fullPath, i + 1)
                    ); ;
                    continue;
                }

                if (
                    sectorData.dataSegments[8] != CoordinationPoint.DATA_NOT_SPECIFIED &&
                    sectorData.dataSegments[9] != CoordinationPoint.DATA_NOT_SPECIFIED
                ) {
                    this.errorLog.AddEvent(
                        new SyntaxError("Cannot set both descend and climb level " + sectorData.dataSegments[0], data.fullPath, i + 1)
                    );
                    continue;
                }

                if (
                    sectorData.dataSegments[8] != CoordinationPoint.DATA_NOT_SPECIFIED &&
                    (!int.TryParse(sectorData.dataSegments[8], out int climbLevel) ||
                    climbLevel < 0)
                ) {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid coordination point climb level " + sectorData.dataSegments[0], data.fullPath, i + 1)
                    );
                    continue;
                }

                if (
                    sectorData.dataSegments[9] != CoordinationPoint.DATA_NOT_SPECIFIED &&
                    (!int.TryParse(sectorData.dataSegments[9], out int descendLevel) ||
                    descendLevel < 0)
                ) {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid coordination point descend level " + sectorData.dataSegments[0], data.fullPath, i + 1)
                    );
                    continue;
                }

                this.sectorElements.Add(
                    new CoordinationPoint(
                        sectorData.dataSegments[0] == CoordinationPoint.POINT_TYPE_FIR,
                        sectorData.dataSegments[1],
                        sectorData.dataSegments[2],
                        sectorData.dataSegments[3],
                        sectorData.dataSegments[4],
                        sectorData.dataSegments[5],
                        sectorData.dataSegments[6],
                        sectorData.dataSegments[7],
                        sectorData.dataSegments[8],
                        sectorData.dataSegments[9],
                        sectorData.dataSegments[10],
                        sectorData.comment
                    )
                );
            }
        }
    }
}
