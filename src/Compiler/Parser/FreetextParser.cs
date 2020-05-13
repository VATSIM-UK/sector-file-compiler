using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;

namespace Compiler.Parser
{
    public class FreetextParser : AbstractSectorElementParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public FreetextParser(
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
                if (sectorData.dataSegments.Count != 4)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Incorrect number of Freetext segments", data.fullPath, i)
                    );
                    continue;
                }

                Coordinate parsedCoordinate = CoordinateParser.Parse(sectorData.dataSegments[0], sectorData.dataSegments[1]);
                if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid coordinate format: " + data.lines[i], data.fullPath, i)
                    );
                    return;
                }


                this.sectorElements.Add(
                    new Freetext(
                        sectorData.dataSegments[2],
                        sectorData.dataSegments[3],
                        parsedCoordinate,
                        sectorData.comment
                    )
                );
            }
        }
    }
}
