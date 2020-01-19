using System.Collections.Generic;
using Compiler.Error;
using Compiler.Event;
using Compiler.Model;

namespace Compiler.Parser
{
    public class LabelParser : AbstractSectorElementParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger eventLogger;

        public LabelParser(
            MetadataParser metadataParser,
            ISectorLineParser sectorLineParser,
            SectorElementCollection sectorElements,
            IEventLogger eventLogger
        ) :base(metadataParser) {
            this.sectorLineParser = sectorLineParser;
            this.sectorElements = sectorElements;
            this.eventLogger = eventLogger;
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
                    this.eventLogger.AddEvent(new SyntaxError("Invalid number of label definition segments", data.fullPath, i + 1));
                    continue;
                }

                // Parse the position coordinate
                Coordinate parsedCoordinate = CoordinateParser.Parse(sectorData.dataSegments[1], sectorData.dataSegments[2]);
                if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid label coordinate format: " + data.lines[i], data.fullPath, i + 1)
                    );
                    continue;
                }

                // Add to the sector elements
                sectorElements.Add(
                    new Label(
                        sectorData.dataSegments[0],
                        parsedCoordinate,
                        sectorData.dataSegments[3],
                        sectorData.comment
                    )
                );
            }
        }
    }
}
