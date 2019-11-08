using System.Collections.Generic;
using Compiler.Error;
using Compiler.Event;
using Compiler.Model;

namespace Compiler.Parser
{
    public class ColourParser : AbstractSectorElementParser
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public ColourParser(
            MetadataParser metadataParser,
            SectorElementCollection sectorElements,
            IEventLogger errorLog
        ) :base(metadataParser) {
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

                string[] parts = data.lines[i].Split(" ");

                if (parts.Length != 3)
                {
                    this.errorLog.AddEvent(new SyntaxError("Invalid number of colour definition segments", data.fileName, i + 1));
                    continue;
                }

                if (parts[0] != "#define")
                {
                    this.errorLog.AddEvent(new SyntaxError("Colour definitions must begin with #define", data.fileName, i + 1));
                    continue;
                }

                if (!int.TryParse(parts[2], out int colourValue))
                {
                    this.errorLog.AddEvent(new SyntaxError("Colour values must be an integer", data.fileName, i + 1));
                    continue;
                }

                sectorElements.Add(new Colour(parts[1], colourValue));
            }
        }
    }
}
