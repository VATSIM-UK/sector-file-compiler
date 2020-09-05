using System.Collections.Generic;
using Compiler.Error;
using Compiler.Event;
using Compiler.Model;
using Compiler.Input;

namespace Compiler.Parser
{
    public class ColourParser : AbstractSectorElementParser, ISectorDataParser
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

                if (sectorData.dataSegments.Count != 3)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid number of colour definition segments", data.FullPath, data.CurrentLineNumber)
                    );
                    continue;
                }

                if (sectorData.dataSegments[0] != "#define")
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Colour definitions must begin with #define", data.FullPath, data.CurrentLineNumber)
                    );
                    continue;
                }

                if (!int.TryParse(sectorData.dataSegments[2].Trim(), out int colourValue))
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Defined colour values must be an integer", data.FullPath, data.CurrentLineNumber)
                    );
                    continue;
                }

                sectorElements.Add(new Colour(sectorData.dataSegments[1], colourValue, sectorData.comment));
            }
        }
    }
}
