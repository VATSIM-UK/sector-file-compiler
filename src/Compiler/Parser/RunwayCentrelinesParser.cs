using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Input;

namespace Compiler.Parser
{
    public class RunwayCentrelinesParser: AbstractSectorElementParser, ISectorDataParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public RunwayCentrelinesParser(
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

        public void ParseData(AbstractSectorDataFile data)
        {
            foreach (string line in data)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(line))
                {
                    continue;
                }

                SectorFormatLine sectorData = this.sectorLineParser.ParseLine(line);
                if (sectorData.dataSegments.Count != 5)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Incorrect number of SIDSTAR segments", data.FullPath, data.CurrentLineNumber)
                    );
                    continue;
                }

                if (sectorData.dataSegments[0] != "SID" && sectorData.dataSegments[0] != "STAR")
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Unknown SIDSTAR type " + sectorData.dataSegments[0], data.FullPath, data.CurrentLineNumber)
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
