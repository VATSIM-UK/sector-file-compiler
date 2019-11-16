using System.Collections.Generic;
using Compiler.Error;
using Compiler.Event;
using Compiler.Model;

namespace Compiler.Parser
{
    public class ColourParser : AbstractSectorElementParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public ColourParser(
            MetadataParser metadataParser,
            ISectorLineParser sectorLineParser,
            SectorElementCollection sectorElements,
            IEventLogger errorLog
        ) :base(metadataParser) {
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

                if (sectorData.dataSegments.Count != 3)
                {
                    this.errorLog.AddEvent(new SyntaxError("Invalid number of colour definition segments", data.fileName, i + 1));
                    continue;
                }

                if (sectorData.dataSegments[0] != "#define")
                {
                    this.errorLog.AddEvent(new SyntaxError("Colour definitions must begin with #define", data.fileName, i + 1));
                    continue;
                }

                if (!int.TryParse(sectorData.dataSegments[2], out int colourValue))
                {
                    this.errorLog.AddEvent(new SyntaxError("Colour values must be an integer", data.fileName, i + 1));
                    continue;
                }

                sectorElements.Add(new Colour(sectorData.dataSegments[1], colourValue, sectorData.comment));
            }
        }
    }
}
