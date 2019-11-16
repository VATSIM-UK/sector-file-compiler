using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;

namespace Compiler.Parser
{
    public class SidStarParser: AbstractSectorElementParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public SidStarParser(
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
                if (sectorData.dataSegments.Count != 5)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Incorrect number of SIDSTAR segments", data.fileName, i)
                    );
                    continue;
                }

                if (sectorData.dataSegments[0] != "SID" && sectorData.dataSegments[0] != "STAR")
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Unknown SIDSTAR type " + sectorData.dataSegments[0], data.fileName, i)
                    ); ;
                    continue;
                }

                this.sectorElements.Add(
                    new SidStar(
                        sectorData.dataSegments[0],
                        sectorData.dataSegments[1],
                        sectorData.dataSegments[2],
                        sectorData.dataSegments[3],
                        new List<string>(sectorData.dataSegments[4].Split(' ')),
                        sectorData.comment
                    )
                );
            }
        }
    }
}
