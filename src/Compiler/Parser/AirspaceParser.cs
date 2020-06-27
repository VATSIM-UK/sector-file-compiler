using Compiler.Error;
using Compiler.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Parser
{
    public class AirspaceParser : AbstractEseAirspaceParser, IFileParser
    {
        private readonly SectorParser sectorParser;
        private readonly SectorlineParser sectorlineParser;
        private readonly CoordinationPointParser coordinationPointParser;
        private readonly ISectorLineParser sectorDataParser;
        private readonly IEventLogger eventLogger;

        public AirspaceParser(
            MetadataParser metadataParser,
            SectorParser sectorParser,
            SectorlineParser sectorlineParser,
            CoordinationPointParser coordinationPointParser,
            ISectorLineParser sectorDataParser,
            IEventLogger eventLogger
        )
            : base(metadataParser)
        {
            this.sectorParser = sectorParser;
            this.sectorlineParser = sectorlineParser;
            this.coordinationPointParser = coordinationPointParser;
            this.sectorDataParser = sectorDataParser;
            this.eventLogger = eventLogger;
        }

        public void ParseData(SectorFormatData data)
        {
            for (int i = 0; i < data.lines.Count;)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(data.lines[i]))
                {
                    i++;
                    continue;
                }

                SectorFormatLine line = this.sectorDataParser.ParseLine(data.lines[i]);
                if (!IsNewDeclaration(line))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid type declaration in AIRSPACE section", data.fullPath, i)
                    );
                    return;
                }


                try
                {
                    switch (line.dataSegments[0])
                    {
                        case "COPX":
                        case "FIR_COPX":
                            i += this.coordinationPointParser.ParseData(data, i);
                            break;
                        case "SECTORLINE":
                        case "CIRCLE_SECTORLINE":
                            i += this.sectorlineParser.ParseData(data, i);
                            break;
                        case "SECTOR":
                            i += this.sectorParser.ParseData(data, i);
                            break;
                    }
                } catch
                {
                    // Do nothing, logged by individual parsers
                    return;
                }
            }
        }
    }
}
