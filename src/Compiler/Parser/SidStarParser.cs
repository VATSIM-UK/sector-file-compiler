using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;

namespace Compiler.Parser
{
    public class SidStarParser: AbstractSectorElementParser
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public SidStarParser(MetadataParser metadataParser, SectorElementCollection sectorElements, IEventLogger errorLog)
            : base(metadataParser)
        {
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

                string[] parts = data.lines[i].Split(':');

                if (parts.Length != 5)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Incorrect number of SIDSTAR segments", data.fileName, i)
                    );
                    continue;
                }

                if (parts[0] != "SID" && parts[0] != "STAR")
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Unknown SIDSTAR type " + parts[0], data.fileName, i)
                    ); ;
                    continue;
                }

                this.sectorElements.Add(
                    new SidStar(parts[0], parts[1], parts[2], parts[3], new List<string>(parts[4].Split(' ')))
                );
            }
        }
    }
}
